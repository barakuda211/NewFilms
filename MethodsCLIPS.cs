using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using CLIPSNET;
using System.Windows.Forms;

namespace NewFilms
{
    public static class MethodsCLIPS
    {
        public static string LoadClipse(CLIPSNET.Environment clips,string clips_text, string filename, List<string> loaded_templates)
        {
            clips.Clear();
            var loaded = RefactorInput(filename, loaded_templates);
            var s = clips_text + "\n"+loaded;
            clips.LoadFromString(s);
            clips.Reset();
            return s;
        }

        private static string RefactorInput(string filename, List<string> loaded_templates)
        {
            
            var lines = File.ReadAllLines(filename, Encoding.UTF8);
            var text = "";
            if (loaded_templates.Count == 0)
            {
                foreach (var line in lines)
                {
                    var words = line.Split(new char[] { '(', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (words.Length > 1 && words[0] == "deftemplate" && !loaded_templates.Contains(words[1]))
                        loaded_templates.Add(words[1]);
                }
                return File.ReadAllText(filename, Encoding.UTF8);
            }
            var rules_to_rename = new List<string>();
            var skip_template = false;
            foreach(var line in lines)
            {
                var words = line.Split(new char[] { '(', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (line.Length>0 && line[0] == ')' && skip_template)
                {
                    skip_template = false;
                    continue;
                }
                if (skip_template)
                    continue;
                if (words.Length>1 && (words[0] == "defrule" || words[0] == "deffacts"))
                    rules_to_rename.Add(words[1]);
                if (words.Length > 1 && words[0] == "deftemplate" && loaded_templates.Contains(words[1]))
                {
                    skip_template = true;
                    continue;
                }
                if (words.Length > 1 && words[0] == "deftemplate" && !loaded_templates.Contains(words[1]))
                    loaded_templates.Add(words[1]);
                text += line + "\n";
            }
            rules_to_rename.Sort((x, y) => x.Length < y.Length ? 1 : -1);
            foreach (var rf in rules_to_rename)
                text = text.Replace(rf, rf + filename);
            return text;
        }

        public static void RunClipse(CLIPSNET.Environment clips, SortedSet<int> fromFacts, Dictionary<int,string> facts, TextBox tb)
        {
            tb.Clear();
            clips.Reset();
            clips.Run();
            HandleResponse(clips, tb);
            foreach (var fact_id in fromFacts)
            {
                var fact = ParseFact(facts[fact_id]);
                clips.Eval($"(assert {fact})");
            }

            bool b = true;
            while (b)
            {
                clips.Run();
                b = HandleResponse(clips,tb);
            }
        }

        private static  bool HandleResponse(CLIPSNET.Environment clips, TextBox tb)
        {
            String evalStr = "(find-fact ((?f ioproxy)) TRUE)";
            FactAddressValue fv = (FactAddressValue)((MultifieldValue)clips.Eval(evalStr))[0];

            MultifieldValue damf = (MultifieldValue)fv["messages"];
            MultifieldValue vamf = (MultifieldValue)fv["answers"];

            for (int i = 0; i < damf.Count; i++)
            {
                LexemeValue da = (LexemeValue)damf[i];
                byte[] bytes = Encoding.Default.GetBytes(da.Value);
                string message = Encoding.UTF8.GetString(bytes);
                tb.Text += message + "\r\n\r\n";

                if (message == "")
                {
                    if (vamf.Count == 0)
                        clips.Eval("(assert (clearmessage))");
                    return false;
                }

            }

            if (damf.Count == 0)
            {
                if (vamf.Count == 0)
                {
                    clips.Eval("(assert (clearmessage))");
                }

                return false;
            }

            if (vamf.Count == 0)
            {
                clips.Eval("(assert (clearmessage))");
            }

            return true;
        }

        private static string ParseFact(string fact)
        {
            var words = fact.Split(new char[] { ' ', '-' });
            var s = "";
            switch (words[0])
            {
                case "Фильм":
                    s = $"(film (data \"{fact}\"))";
                    break;
                case "Режиссёр":
                    s = $"(director (data \"{fact}\"))";
                    break;
                case "Рейтинг":
                    s = $"(rating (data \"{fact}\"))";
                    break;
                case "Композитор":
                    s = $"(composer (data \"{fact}\"))";
                    break;
                case "12+":
                case "16+":
                case "18+":
                    s = $"(age (data \"{fact}\"))";
                    break;
                case "Есть":
                case "Нет":
                    s = $"(oscar (data \"{fact}\"))";
                    break;
                case "Женщина":
                case "Мужчина":
                    s = $"(gender (data \"{fact}\"))";
                    break;
                case "На":
                    s = $"(mood (data \"{fact}\"))";
                    break;
                default:
                    var tmp = int.TryParse(words[0], out int res) ? "time_space" : "genre";
                    s = $"({tmp} (data \"{fact}\"))";
                    break;
            }
            return s;
        }

        private static string ParseFactCoefs(string fact)
        {
            var words = fact.Split(new char[] { ' ', '-' });
            var s = "";
            switch (words[0])
            {
                case "Фильм":
                    s = $"(film (data \"{fact}\") (weight ?film_w))";
                    break;
                case "Режиссёр":
                    s = $"(director (data \"{fact}\") (weight ?director_w))";
                    break;
                case "Рейтинг":
                    s = $"(rating (data \"{fact}\") (weight ?rating_w))";
                    break;
                case "Композитор":
                    s = $"(composer (data \"{fact}\") (weight ?composer_w))";
                    break;
                case "12+":
                case "16+":
                case "18+":
                    s = $"(age (data \"{fact}\") (weight ?age_w))";
                    break;
                case "Есть":
                case "Нет":
                    s = $"(oscar (data \"{fact}\") (weight ?oscar_w))";
                    break;
                case "Женщина":
                case "Мужчина":
                    s = $"(gender (data \"{fact}\") (weight ?gender_w))";
                    break;
                case "На":
                    s = $"(mood (data \"{fact}\") (weight ?mood_w))";
                    break;
                default:
                    var tmp = int.TryParse(words[0], out int res1) ? "time_space" : "genre";
                    var w = "?" + tmp + "_w";
                    s = $"({tmp} (data \"{fact}\") (weight {w}))";
                    break;
            }
            return s;
        }

        private static string ParseFactWeight(string fact)
        {
            var words = fact.Split(new char[] { ' ', '-' });
            var s = "";
            switch (words[0])
            {
                case "Фильм":
                    s = $"?film_w";
                    break;
                case "Режиссёр":
                    s = $"?director_w";
                    break;
                case "Рейтинг":
                    s = $"?rating_w";
                    break;
                case "Композитор":
                    s = $"?composer_w";
                    break;
                case "12+":
                case "16+":
                case "18+":
                    s = $"?age_w";
                    break;
                case "Есть":
                case "Нет":
                    s = $"?oscar_w";
                    break;
                case "Женщина":
                case "Мужчина":
                    s = $"?gender_w";
                    break;
                case "На":
                    s = $"?mood_w";
                    break;
                default:
                    s = int.TryParse(words[0], out int res1) ? "?time_space_w" : "?genre_w";
                    break;
            }
            return s;
        }

        private static (double,string) GetWeight(string[] from)
        {
            var from_first = from.Select(x => x.Split(new char[] { ' ', '-' })[0]).ToArray();
            var w = 0.0; var s = "";
            if (from_first.Length == 1)
            {
                switch (from_first[0])
                {
                    case "Женщина":
                    case "Мужчина":
                        w = 0.7;
                        break;
                    case "12+":
                    case "16+":
                    case "18+":
                        w = 0.6;
                        break;
                    case "На":
                        w = 0.7;
                        break;
                    case "Есть":
                    case "Нет":
                        w = 0.6;
                        break;
                    case "Композитор":
                        w = 0.8;
                        break;
                }
                s = $"(* {w.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))} {ParseFactWeight(from_first.First())})";
                return (w, s);
            }
            if (from_first.Length == 3)
            {
                w = 0.9;
                s = $"(* {w.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))} {ParseFactWeight(from_first[0])} {ParseFactWeight(from_first[1])} {ParseFactWeight(from_first[2])})";
                return (w, s);
            }
            return (0.9, $"(* 0.9 {ParseFactWeight(from_first[0])} {ParseFactWeight(from_first[1])} {ParseFactWeight(from_first[2])} {ParseFactWeight(from_first[3])})");
        }

        public static void MakeCLIPSFile(Dictionary<int, string> facts, MultiDictionary rules, string filename = "genearted_clips.clp")
        {
            var sb = new StringBuilder(Properties.Resources.CLIPS_init);
            //sb.Append("(deffacts film_selection\n");

            var CLIPS_facts = new Dictionary<int, string>();
            foreach (var fact in facts)
            {
                var s = ParseFact(fact.Value);
                CLIPS_facts[fact.Key] = s;
            }
            sb.Append("\n\n");

            int rule_number = 0;
            foreach (var key in rules.data.Keys)
            {
                var to_fact = CLIPS_facts[key];
                foreach (var from_set in rules[key] )
                {
                    sb.Append($"(defrule rule{rule_number++}\n");
                    var s = "";
                    foreach (var from in from_set)
                    {
                        s += facts[from] + ", ";
                        sb.Append(CLIPS_facts[from] + "\n");
                    }
                    s = s.Substring(0, s.Length - 2);
                    s += " => " + facts[key];
                    sb.Append($"=>\n");
                    sb.Append($"(assert{to_fact})\n");
                    sb.Append($"(assert (appendmessagehalt \"{s}\"))\n)\n\n");
                }
            }

            File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);
        }

        public static void MakeCLIPSCoefFile(Dictionary<int, string> facts, MultiDictionary rules, string filename = "genearted_clips_coef.clp")
        {
            var sb = new StringBuilder(Properties.Resources.CLIPS_coef_init);
            //sb.Append("(deffacts film_selection\n");

            var CLIPS_facts = new Dictionary<int, string>();
            foreach (var fact in facts)
            {
                var s = ParseFactCoefs(fact.Value);
                CLIPS_facts[fact.Key] = s;
            }
            sb.Append("\n\n");

            int rule_number = 0;
            foreach (var key in rules.data.Keys)
            {
                var to_fact = CLIPS_facts[key];
                foreach (var from_set in rules[key])
                {
                    sb.Append($"(defrule rule{rule_number++}\n");
                    var s = "";
                    foreach (var from in from_set)
                    {
                        s += facts[from] + $"(\"{ParseFactWeight(facts[from])}\"), ";
                        sb.Append(CLIPS_facts[from] + "\n");
                        sb.Append($"(test ( < 0.3 {ParseFactWeight(facts[from])} ))\n");
                    }
                    s = s.Substring(0, s.Length - 2);
                    s += " => " + facts[key];
                    var weight = GetWeight(from_set.Select(x => facts[x]).ToArray());
                    sb.Append($"(test ( < 0.3 {weight.Item2} ))\n");
                    sb.Append($"=>\n");
                    var line = to_fact.Replace(ParseFactWeight(facts[key]), weight.Item2);
                    sb.Append($"(assert{line})\n");
                    s += $"(\" {weight.Item2} \")";
                    sb.Append($"(assert (appendmessagehalt (str-cat \" {s} \")))\n)\n\n");
                }
            }

            File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);
        }

        public static void RunClipseCoef(CLIPSNET.Environment clips, List<string> fromFacts, List<double> coefs, Dictionary<int, string> facts, TextBox tb)
        {
            tb.Clear();
            clips.Reset();
            clips.Run();
            HandleResponse(clips, tb);
            for(int i = 0; i < fromFacts.Count; i++)
            {
                var fact = fromFacts[i];
                var line = ParseFactCoefs(fact).Replace(ParseFactWeight(fact),coefs[i].ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
                clips.Eval($"(assert {line})");
            }

            bool b = true;
            while (b)
            {
                clips.Run();
                b = HandleResponse(clips, tb);
            }
        }
    }
}
