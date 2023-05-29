using System.Globalization;

public class PackageOptimizer
{
    static void Main(string[] args)
    {
        string filename = "example_input.txt";
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split(':');
            int weightLimit = int.Parse(parts[0].Trim());
            string itemsStr = parts[1].Trim();

            List<Item> items = ParseItems(itemsStr);
            List<int> selectedItems = OptimizePackage(weightLimit, items);
            string result = string.Join(",", selectedItems);

            Console.WriteLine(result);
        }
    }

    public static List<Item> ParseItems(string itemsStr)
    {
        List<Item> items = new List<Item>();
        string[] itemStrs = itemsStr.Split('(', ')', ' ');

        for (int i = 0; i < itemStrs.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(itemStrs[i]))
            {
                string[] itemParts = itemStrs[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
                int index = int.Parse(itemParts[0]);
                double weight = double.Parse(itemParts[1], CultureInfo.InvariantCulture);
                int cost = int.Parse(itemParts[2].Substring(1));

                Item item = new Item(index, weight, cost);
                items.Add(item);
            }
        }

        return items;
    }

    public static List<int> OptimizePackage(int weightLimit, List<Item> items)
    {
        int n = items.Count;
        int[,] dp = new int[n + 1, weightLimit + 1];
        bool[,] selected = new bool[n + 1, weightLimit + 1];

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= weightLimit; j++)
            {
                Item item = items[i - 1];

                if (item.Weight <= j)
                {
                    int remainingWeight = j - (int)item.Weight;

                    int newValue = item.Cost + dp[i - 1, remainingWeight];
                    int prevValue = dp[i - 1, j];

                    if (newValue > prevValue)
                    {
                        dp[i, j] = newValue;
                        selected[i, j] = true;
                    }
                    else if (newValue == prevValue)
                    {
                        // Select the item with lower weight
                        Item prevItem = items[i - 2]; // Get the previously selected item
                        if (item.Weight < prevItem.Weight)
                        {
                            dp[i, j] = newValue;
                            selected[i, j] = true;
                        }
                        else
                        {
                            dp[i, j] = prevValue;
                        }
                    }
                    else
                    {
                        dp[i, j] = prevValue;
                    }
                }
                else
                {
                    dp[i, j] = dp[i - 1, j];
                }
            }
        }

        List<int> selectedItems = new List<int>();
        int row = n;
        int col = weightLimit;

        while (row > 0 && col > 0)
        {
            if (selected[row, col])
            {
                selectedItems.Add(row);
                col -= (int)items[row - 1].Weight;
            }

            row--;
        }

        selectedItems.Reverse();
        return selectedItems;
    }
    public class Item
    {
        public int Index { get; }
        public double Weight { get; }
        public int Cost { get; }

        public Item(int index, double weight, int cost)
        {
            Index = index;
            Weight = weight;
            Cost = cost;
        }
    }

}



