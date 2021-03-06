using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewFilms
{
    public partial class Form1 : Form
    {
        string fileName = "db.txt";
        Dictionary<int, string> facts = new Dictionary<int, string>();
        MultiDictionary rules = new MultiDictionary();
        List<Button> tableButtons = new List<Button>();
        List<TextBox> tableCoefs = new List<TextBox>();
        SortedSet<int> fromFacts = new SortedSet<int>();
        string clips_text = "";
        List<string> loaded_templates = new List<string>();
        int count_loaded = 0;
        int tofact = -1;

        private CLIPSNET.Environment clips = new CLIPSNET.Environment();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadFile();
            fromComboBox.Items.AddRange(facts.Values.Where(s => s.Split(' ').First() != "Фильм").ToArray());
            toComboBox.Items.AddRange(facts.Values.Where(s => s.Split(' ').First() == "Фильм").ToArray());
        }

        private void ReadFile()
        {
            var lines = File.ReadAllLines(fileName).Select(
                s => s.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)).ToArray(); ;
            int id = -1, i = 0;
            while (int.TryParse(lines[i][0].Substring(2), out id))
            {
                facts.Add(id, lines[i][1]);
                i++;
            }
            i++;
            while (i != lines.Count())
            {
                var from = new SortedSet<int>();
                for (int j = 1; j < lines[i].Count() - 1; j++)
                    from.Add(int.Parse(lines[i][j].Substring(2)));
                rules.Add(int.Parse(lines[i].Last().Substring(2)), from);
                i++;
            }
        }

        private void RedrawTables()
        {
            int i = 0;
            table.Controls.Clear();
            foreach (var b in tableButtons)
            {
                b.Tag = i;
                table.Controls.Add(b, i, 0);
                i++;
            }

            i = 0;
            table_coef.Controls.Clear();
            foreach (var b in tableCoefs)
            {
                b.Tag = i;
                table_coef.Controls.Add(b, i, 0);
                i++;
            }
        }

        private void tableButton_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;
            var index = (b.Tag as int?).Value;
            //удаляем из множества рассматриваемых фактов
            fromFacts.Remove(facts.First(pair => pair.Value == b.Text).Key);
            tableButtons.RemoveAt(index);
            tableCoefs.RemoveAt(index);
            RedrawTables();
        }

        private void fromComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tableButtons.Count == table.RowCount)
                return;
            var b = new Button();
            b.Parent = table;
            b.Dock = DockStyle.Fill;
            b.Text = (string)fromComboBox.SelectedItem;
            b.Tag = tableButtons.Count;
            b.Click += tableButton_Click;
            tableButtons.Add(b);
            RedrawTables();

            var tb = new TextBox();
            tb.Text = "1,0";
            tb.Parent = table_coef;
            tb.Tag = tableCoefs.Count;
            tableCoefs.Add(tb);
            RedrawTables();

            //добавляем в множество рассматриваемых фактов
            fromFacts.Add(facts.First(pair => pair.Value == b.Text).Key);
        }


        private void toComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tofact = facts.First(pair => pair.Value == (string)toComboBox.SelectedItem).Key;
        }

        class node
        {
            public SortedSet<int> data;
            public string output;

            public node(SortedSet<int> data, string output)
            {
                this.data = data;
                this.output = output;
            }

            public node(node nd)
            {
                data = new SortedSet<int>(nd.data);
                //output = new string(nd.output);
                output = string.Copy(nd.output);
            }
        }

        private void ForwardChaining()
        {
            outputTextBox.Clear();
            var root = new node(fromFacts, "");
            var q = new Queue<node>();
            var has_result = false;
            var founded = new List<int>();
            q.Enqueue(root);
            var iteration = 0;
            while (q.Count > 0 && iteration < 100)
            {
                iteration++;
                var nd = q.Dequeue();
                var lst = rules.GetValues(nd.data);
                foreach (var rule in lst)
                {
                    var set = new SortedSet<int>(nd.data);
                    if (!set.Add(rule.Item1))
                        continue;
                    //var info = new string(nd.output);
                    var info = string.Copy(nd.output);
                    foreach (var id in rule.Item2)
                        info += facts[id] + ", ";
                    info += "=>" + facts[rule.Item1] + "\r\n\r\n";
                    if (!founded.Contains(rule.Item1) && facts[rule.Item1].Split(' ').First() == "Фильм")
                    {
                        founded.Add(rule.Item1);
                        outputTextBox.Text += info + "\r\n";
                        has_result = true;
                        continue;
                    }
                    q.Enqueue(new node(set, info));
                }
            }
            if (!has_result)
                outputTextBox.Text = "Нет вывода!";
            outputTextBox.Refresh();
        }

        private void BackwardChaining()
        {
            outputTextBox.Clear();
            var root_set = new SortedSet<int>(); root_set.Add(tofact);
            var root = new node(root_set, "");
            var q = new Queue<node>();
            q.Enqueue(root);
            int it = 0;
            while (q.Count > 0 && it < 100)
            {
                it++;
                var nd = q.Dequeue();
                foreach (var id in nd.data)
                {
                    var lst = rules[id];
                    foreach (var set in lst)
                    {
                        //var info = new string(nd.output);
                        var info = string.Copy(nd.output);
                        info += "(" + facts[id] + "=>";
                        foreach (var x in set)
                            info += facts[x] + ", ";
                        info += ")\r\n =>";
                        var new_set = new SortedSet<int>(nd.data);
                        new_set.Remove(id);
                        foreach (var x in set)
                            new_set.Add(x);
                        foreach (var x in new_set)
                            info += facts[x] + ", ";
                        info += "\r\n\r\n";
                        //if (new_set.Intersect(fromFacts).Count() == new_set.Count)
                        if (ContainsAll(new_set,fromFacts))
                        {
                            outputTextBox.Text = info;
                            outputTextBox.Refresh();
                            return;
                        }
                        q.Enqueue(new node(new_set, info));
                    }
                }
            }
            outputTextBox.Text = "Нет вывода!";
            outputTextBox.Refresh();
        }

        private bool ContainsAll(SortedSet<int> sub_s, SortedSet<int> s)
        {
            foreach (var x in sub_s)
                if (!s.Contains(x))
                    return false;
            return true;
        }

        private void doButton_Click(object sender, EventArgs e)
        {
            if (fromFacts.Count == 0)
                return;
            ForwardChaining();
        }

        private void reverseDoButton_Click(object sender, EventArgs e)
        {
            if (fromFacts.Count == 0 || tofact == -1)
                return;
            BackwardChaining();
        }

        private void generate_clips_button_Click(object sender, EventArgs e)
        {
            MethodsCLIPS.MakeCLIPSFile(facts, rules);
        }

        private void load_clipse_button_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                clips_text = MethodsCLIPS.LoadClipse(clips, clips_text, ofd.FileName,loaded_templates);
                run_clipse_button.Enabled = true;
                run_clips_coef_button.Enabled = true;
            }
        }

        private void run_clipse_button_Click(object sender, EventArgs e)
        {
            MethodsCLIPS.RunClipse(clips, fromFacts, facts, outputTextBox);
        }

        private void generate_clips_coef_button_Click(object sender, EventArgs e)
        {
            MethodsCLIPS.MakeCLIPSCoefFile(facts, rules);
        }

        private void run_clips_coef_button_Click(object sender, EventArgs e)
        {
            var fc = new List<string>();
            var coefs = new List<double>();
            foreach (var tb in tableButtons)
                fc.Add(tb.Text);
            foreach (var tb in tableCoefs)
                coefs.Add(double.Parse(tb.Text));
            MethodsCLIPS.RunClipseCoef(clips, fc, coefs, facts, outputTextBox);
        }
    }

    public class MultiDictionary
    {
        public Dictionary<int, List<SortedSet<int>>> data = new Dictionary<int, List<SortedSet<int>>>();

        public void Add(int key, SortedSet<int> values)
        {
            if (!data.ContainsKey(key))
                data[key] = new List<SortedSet<int>>();
            data[key].Add(values);
        }

        public bool HasKey(int key) => data.ContainsKey(key);

        public (int, SortedSet<int>) GetValue(SortedSet<int> value)
        {
            foreach (var lst in data)
                foreach (var set in lst.Value)
                    if (set.Intersect(value).Count() == set.Count)
                        return (lst.Key, set);
            return (-1, null);
        }

        public List<(int, SortedSet<int>)> GetValues(SortedSet<int> value)
        {
            var res = new List<(int, SortedSet<int>)>();
            foreach (var lst in data)
                foreach (var set in lst.Value)
                    if (set.Intersect(value).Count() == set.Count)
                        res.Add((lst.Key, set));
            return res;
        }

        public void Clear()
        {
            data.Clear();
        }

        public List<SortedSet<int>> this[int key]
        {
            get
            {
                if (data.ContainsKey(key))
                    return data[key];
                return new List<SortedSet<int>>();
            }
        }
    }
}
