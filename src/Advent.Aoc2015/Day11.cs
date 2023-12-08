namespace Advent.Aoc2015
{
    public class Day11(IInput input)
    {
        public void Part1()
        {
            char[] currentPassword = input.GetSingleLine().ToCharArray();
            while (!IsValidPassword(currentPassword))
            {
                IncrementPassword(currentPassword);
            }

            Console.WriteLine(string.Join(string.Empty, currentPassword));
        }

        public void Part2()
        {
            char[] currentPassword = input.GetSingleLine().ToCharArray();
            while (!IsValidPassword(currentPassword))
            {
                IncrementPassword(currentPassword);
            }

            IncrementPassword(currentPassword);
            while (!IsValidPassword(currentPassword))
            {
                IncrementPassword(currentPassword);
            }

            Console.WriteLine(string.Join(string.Empty, currentPassword));
        }

        private bool IsValidPassword(char[] currentPassword)
        {
            return HasThreeConsecutiveCharacters(currentPassword) && !HasForbiddenCharacters(currentPassword) && HasTwoPairs(currentPassword);
        }

        private bool HasThreeConsecutiveCharacters(char[] currentPassword)
        {
            for (int i = 0; i < currentPassword.Length - 2; i++)
            {
                if (currentPassword[i] + 1 == currentPassword[i + 1] && currentPassword[i] + 2 == currentPassword[i + 2])
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasForbiddenCharacters(char[] currentPassword)
        {
            return currentPassword.Any(c => c is 'i' or 'o' or 'l');
        }

        private bool HasTwoPairs(char[] currentPassword)
        {
            int pairs = 0;
            for (int i = 0; i < currentPassword.Length - 1; i++)
            {
                if (currentPassword[i] == currentPassword[i + 1])
                {
                    pairs++;
                    i++;
                }
            }

            return pairs >= 2;
        }

        private void IncrementPassword(char[] currentPassword)
        {
            int i = currentPassword.Length - 1;
            while (currentPassword[i] == 'z')
            {
                currentPassword[i--] = 'a';
            }

            currentPassword[i]++;
        }
    }
}