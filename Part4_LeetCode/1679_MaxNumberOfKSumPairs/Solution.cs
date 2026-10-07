public class Solution {
    public int MaxOperations(int[] nums, int k)
    {
        Dictionary<int, int> frequency = new();

        int operations = 0;

        foreach (int num in nums)
        {
            int complement = k - num;

            if (frequency.ContainsKey(complement) && frequency[complement] > 0)
            {
                frequency[complement]--;
                operations++;
            }
            else
            {
                if (frequency.ContainsKey(num))
                    frequency[num]++;
                else
                    frequency[num] = 1;
            }
        }

        return operations;
    }
}