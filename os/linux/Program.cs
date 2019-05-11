using System;
using System.Text.RegularExpressions;
using System.IO;

namespace Linux
{
    class LinuxMemInfo
    {
        public long GetMemorySize()
        {
            FreeCSharp free = new FreeCSharp();
            free.GetValues();
            return free.MemTotal;
        }
    }

    public class FreeCSharp
    {
        public long MemTotal { get; private set; }
        public long MemFree { get; private set; }
        public long Buffers { get; private set; }
        public long Cached { get; private set; }

        public void GetValues()
        {
            string[] memInfoLines = File.ReadAllLines(@"/proc/meminfo");

            MemInfoMatch[] memInfoMatches =
            {
                new MemInfoMatch(@"^Buffers:\s+(\d+)", value => Buffers = Convert.ToInt64(value)),
                new MemInfoMatch(@"^Cached:\s+(\d+)", value => Cached = Convert.ToInt64(value)),
                new MemInfoMatch(@"^MemFree:\s+(\d+)", value => MemFree = Convert.ToInt64(value)),
                new MemInfoMatch(@"^MemTotal:\s+(\d+)", value => MemTotal = Convert.ToInt64(value))
            };

            foreach (string memInfoLine in memInfoLines)
            {
                foreach (MemInfoMatch memInfoMatch in memInfoMatches)
                {
                    Match match = memInfoMatch.regex.Match(memInfoLine);
                    if (match.Groups[1].Success)
                    {
                        string value = match.Groups[1].Value;
                        memInfoMatch.updateValue(value);
                    }
                }
            }
        }

        public class MemInfoMatch
        {
            public Regex regex;
            public Action<string> updateValue;

            public MemInfoMatch(string pattern, Action<string> update)
            {
                this.regex = new Regex(pattern, RegexOptions.Compiled);
                this.updateValue = update;
            }
        }
    }
}