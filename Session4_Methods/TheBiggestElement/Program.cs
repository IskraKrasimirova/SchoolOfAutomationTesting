namespace BiggestElement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { -3, 5, 7, -2, 8, int.MinValue, 1, 4, 0, -123456789 };

            // Using LINQ
            //int biggestElement = array.Max();
            //Console.WriteLine($"The biggest element in the array is {biggestElement}.");

            //int[] sortedArray = array.OrderByDescending(x => x).ToArray();

            //Console.WriteLine($"Sorted descending array: {string.Join(", ", sortedArray)}");

            // ****************************************************************
            // Using method that returns index of the biggest element with start index
            //var maxIndex = FindMaxElementIndex(array, 0);
            //Console.WriteLine($"The biggest element in the array is {array[maxIndex]} at index {maxIndex}.");

            //for (int i = 0; i < array.Length; i++)
            //{
            //    var maxIdx = FindMaxElementIndex(array, i);

            //    //int temp = array[i];
            //    //array[i] = array[maxIdx];
            //    //array[maxIdx] = temp;

            //    // tuple swap from C# 7.0
            //    (array[maxIdx], array[i]) = (array[i], array[maxIdx]);
            //}

            //Console.WriteLine($"Sorted descending array: {string.Join(", ", array)}");

            // ****************************************************************
            // Using method that returns index of the biggest element
            var maxIndex = FindMaxElementIndex(array);
            Console.WriteLine($"The biggest element in the array is {array[maxIndex]} at index {maxIndex}.");

            int[] sortedDescArray = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                int maxIdx = FindMaxElementIndex(array);
                sortedDescArray[i] = array[maxIdx];
                array[maxIdx] = int.MinValue;
            }

            Console.WriteLine($"Sorted descending array: {string.Join(", ", sortedDescArray)}");
        }

        private static int FindMaxElementIndex(int[] array)
        {
            int maxIndex = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        private static int FindMaxElementIndex(int[] array, int startIndex)
        {
            int maxIndex = startIndex;

            for (int i = startIndex + 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        private static (int index, int maxValue) FindMaxElementAndIndex(int[] array)
        {
            if (array == null || array.Length == 0)
            {
                Console.WriteLine("Array cannot be null or empty.");
            }

            int maxIndex = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }

            return (maxIndex, array[maxIndex]);
        }
    }
}

//6.Write a method that finds the biggest element of an array. Use that method to implement sorting in descending order.