namespace Advent.Aoc2022
{
    public class Day07
    {
        private const string root = "/";
        private const string pathSeparator = "/";
        private readonly IInput input;

        public Day07(IInput input)
        {
            this.input = input;
        }

        public int Part1()
        {
            var lines = input.GetLines().ToList();
            var directories = ParseCommandlineLog(lines);

            return directories.Values.Where(dir => dir.Size <= 100000).Sum(dir => dir.Size);
        }

        public int Part2()
        {
            var lines = input.GetLines().ToList();
            var directories = ParseCommandlineLog(lines);
            int requiredSpace = directories[root].Size - 40000000;

            return directories.Values.Where(dir => dir.Size >= requiredSpace).Min(dir => dir.Size);
        }

        private static Dictionary<string, Directory> ParseCommandlineLog(List<string> lines)
        {
            string currentPath = string.Empty;
            var directories = new Dictionary<string, Directory> { [root] = new Directory(root) };
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                if (line.StartsWith("$ cd /"))
                {
                    currentPath = root;
                }
                else if (line == "$ cd ..")
                {
                    currentPath = currentPath[..currentPath[..^1].LastIndexOf(pathSeparator)] + pathSeparator;
                }
                else if (line.StartsWith("$ cd "))
                {
                    currentPath += line[5..] + pathSeparator;
                }
                else if (line.StartsWith("$ ls"))
                {
                    // noop
                }
                else if (line.StartsWith("dir "))
                {
                    string name = line[4..];
                    var directory = new Directory(name);
                    directories[currentPath + name + pathSeparator] = directory;
                    directories[currentPath].Items.Add(directory);
                }
                else
                {
                    string[] parts = line.Split(' ');
                    var file = new File(parts[1], int.Parse(parts[0]));
                    directories[currentPath].Items.Add(file);
                }
            }

            return directories;
        }

        private interface IGetSize
        {
            int Size { get; }
        }

        private class Directory : IGetSize
        {
            public string Name { get; }
            public List<IGetSize> Items { get; set; } = new List<IGetSize>();
            public int Size => Items.Sum(i => i.Size);

            public Directory(string name)
            {
                Name = name;
            }
        }

        private class File : IGetSize
        {
            public string Name { get; }
            public int Size { get; }

            public File(string name, int size)
            {
                Name = name;
                Size = size;
            }
        }
    }
}