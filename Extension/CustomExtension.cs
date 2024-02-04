namespace EMS.Extension
{
    public static class CustomExtension
    {
        public static List<T> Filter<T>(this List<T> records, Func<T, bool> func)
        {
            List<T> filteredList = new List<T>();

            foreach (T record in records)
            {
                filteredList.Add(record);
            }
            return filteredList;
        }
    }
}
