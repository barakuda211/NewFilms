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
        public static bool LoadClipse(CLIPSNET.Environment clips)
        {
            clips.Clear();
            if (!File.Exists("genearted_clips.clp"))
                return false;
            var s = File.ReadAllText("genearted_clips.clp", Encoding.UTF8);
            clips.LoadFromString(s);
            clips.Reset();
            return true;
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
            sb.Append(")\n\n");

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
    }
}
