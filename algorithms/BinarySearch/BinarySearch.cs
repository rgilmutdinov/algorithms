using System.Security.Authentication.ExtendedProtection;

namespace algorithms.BinarySearch
{
    /*
     * These 3 templates differ by their:
     * 
     * left, mid, right index assignments
     * loop or recursive termination condition
     * necessity of post-processing
     * Template 1 and 3 are the most commonly used and almost all binary search problems can be easily implemented in one of them.
     * Template 2 is a bit more advanced and used for certain types of problems.       
     */
    public class BinarySearch
    {
        /*
         * Template #1 is the most basic and elementary form of Binary Search.
         * It is the standard Binary Search Template that most high schools or universities use when they first teach students computer science.
         * Template #1 is used to search for an element or condition which can be determined by accessing a single index in the array.
         * 
         * Key Attributes:
         * 
         * Most basic and elementary form of Binary Search
         * Search Condition can be determined without comparing to the element's neighbors (or use specific elements around it)
         * No post-processing required because at each step, you are checking to see if the element has been found. If you reach the end, then you know the element is not found
         * 
         * 
         * Distinguishing Syntax:
         * 
         * Initial Condition: left = 0, right = length-1
         * Termination: left > right
         * Searching Left: right = mid-1
         * Searching Right: left = mid+1
         */
        public int BinarySearch1(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
            {
                return -1;
            }

            int lo = 0, hi = nums.Length - 1;
            while (lo <= hi)
            {
                // Prevent (lo + hi) overflow
                int mid = lo + (hi - lo) / 2;
                if (nums[mid] == target)
                {
                    return mid;
                }

                if (nums[mid] < target)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid - 1;
                }
            }

            // End Condition: left > hi
            return -1;
        }


        /*
         * Template #2 is an advanced form of Binary Search.
         * It is used to search for an element or condition which requires accessing the current index and its immediate right neighbor's index in the array.
         * 
         * Key Attributes:
         * 
         * An advanced way to implement Binary Search.
         * Search Condition needs to access element's immediate right neighbor
         * Use element's right neighbor to determine if condition is met and decide whether to go left or right
         * Gurantees Search Space is at least 2 in size at each step
         * Post-processing required. Loop/Recursion ends when you have 1 element left. Need to assess if the remaining element meets the condition.
         * 
         * 
         * Distinguishing Syntax:
         * 
         * Initial Condition: left = 0, right = length
         * Termination: left == right
         * Searching Left: right = mid
         * Searching Right: left = mid+1
         */
        public int BinarySearch2(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
            {
                return -1;
            }

            int lo = 0, hi = nums.Length;
            while (lo < hi)
            {
                // Prevent (lo + hi) overflow
                int mid = lo + (hi - lo) / 2;
                if (nums[mid] == target)
                {
                    return mid;
                }

                if (nums[mid] < target)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid;
                }
            }

            // Post-processing:
            // End Condition: lo == hi
            if (lo != nums.Length && nums[lo] == target)
            {
                return lo;
            }

            return -1;
        }

        /*
         * Template #3 is another unique form of Binary Search.
         * It is used to search for an element or condition which requires accessing the current index and
         * its immediate left and right neighbor's index in the array.
         * 
         * Key Attributes:
         * 
         * An alternative way to implement Binary Search
         * Search Condition needs to access element's immediate left and right neighbors
         * Use element's neighbors to determine if condition is met and decide whether to go left or right
         * Gurantees Search Space is at least 3 in size at each step
         * Post-processing required. Loop/Recursion ends when you have 2 elements left. Need to assess if the remaining elements meet the condition.
         * 
         * 
         * Distinguishing Syntax:
         * 
         * Initial Condition: left = 0, right = length-1
         * Termination: left + 1 == right
         * Searching Left: right = mid
         * Searching Right: left = mid
         */
        public int BinarySearch3(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
            {
                return -1;
            }

            int lo = 0, hi = nums.Length - 1;
            while (lo + 1 < hi)
            {
                // Prevent (lo + hi) overflow
                int mid = lo + (hi - lo) / 2;
                if (nums[mid] == target)
                {
                    return mid;
                }

                if (nums[mid] < target)
                {
                    lo = mid;
                }
                else
                {
                    hi = mid;
                }
            }

            // Post-processing:
            // End Condition: lo + 1 == hi
            if (nums[lo] == target) return lo;
            if (nums[hi] == target) return hi;

            return -1;
        }

        public int BisectLeft(int[] nums, int target)
        {
            int lo = 0, hi = nums.Length - 1;
            while (lo < hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (nums[mid] < target)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid;
                }
            }

            return lo;
        }
    }
}
