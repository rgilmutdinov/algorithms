namespace algorithms
{
    public static class Permutation
    {
        private static void Swap(int[] nums, int i, int j)
        {
            int tmp = nums[i];
            nums[i] = nums[j];
            nums[j] = tmp;
        }

        public static void Next(int[] nums)
        {
            if (nums == null || nums.Length < 2) return;

            bool reverse = true;
            for (int i = nums.Length - 1; i > 0; i--)
            {
                if (nums[i] > nums[i - 1])
                {
                    reverse = false;
                    int larger = i;
                    for (int j = nums.Length - 1; j > i; j--)
                    {
                        if (nums[j] > nums[i - 1])
                        {
                            larger = j;
                            break;
                        }
                    }

                    Swap(nums, i - 1, larger);

                    int start = i, stop = nums.Length - 1;
                    while (start < stop)
                    {
                        Swap(nums, start, stop);
                        start++;
                        stop--;
                    }

                    break;
                }
            }

            if (reverse)
            {
                int start = 0, stop = nums.Length - 1;
                while (start < stop)
                {
                    Swap(nums, start, stop);
                    start++;
                    stop--;
                }
            }
        }
    }
}
