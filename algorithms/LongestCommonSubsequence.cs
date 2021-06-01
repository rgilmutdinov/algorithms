using System;

namespace algorithms
{
    public static class LongestCommonSubsequence
    {
        public static int Get1(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;

            int[] dp = new int[n + 1];
            for (int i = 0; i <= m; i++)
            {
                int[] temp = new int[n + 1];
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0) continue;
                    if (s1[i - 1] == s2[j - 1])
                    {
                        temp[j] = dp[j - 1] + 1;
                    }
                    else
                    {
                        temp[j] = Math.Max(dp[j], temp[j - 1]);
                    }
                }
                dp = temp;
            }

            return dp[n];
        }

        public static int Get2(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0) continue;
                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            return dp[m, n];
        }

        public static int Get3(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            int[,] cache = new int[m + 1, n + 1];
            return LCS(cache, s1, s2, m, n);
        }

        public static int LCS(int[,] cache, string s1, string s2, int m, int n)
        {
            if (m == 0 || n == 0) return 0;
            if (cache[m, n] > 0)
            {
                return cache[m, n];
            }

            int len;
            if (s1[m - 1] == s2[n - 1])
            {
                len = 1 + LCS(cache, s1, s2, m - 1, n - 1);
            }
            else
            {
                len = Math.Max(LCS(cache, s1, s2, m - 1, n), LCS(cache, s1, s2, m, n - 1));
            }

            cache[m, n] = len;
            return len;
        }
    }
}
