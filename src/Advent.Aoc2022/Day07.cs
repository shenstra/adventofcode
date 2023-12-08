namespace Advent.Aoc2022
{
    public class Day07(IInput input)
    {
        private const string root = "/";
        private const string pathSeparator = "/";

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

        private class Directory(string name) : IGetSize
        {
            public string Name { get; } = name;
            public List<IGetSize> Items { get; set; } = [];
            public int Size => Items.Sum(i => i.Size);
        }

        private class File(string name, int size) : IGetSize
        {
            public string Name { get; } = name;
            public int Size { get; } = size;
        }
    }
}