namespace Game.Core
{
    public static class ArrayExtensions
    {
        public static T[] AddElement<T>(this T[] source, T element)
        {
            if (source == null)
            {
                return new T[] {element};
            }

            T[] updated = new T[source.Length + 1];
            for (int i = 0; i < source.Length; i++)
            {
                T nextElement = source[i];
                updated[i] = nextElement;
            }

            updated[updated.Length - 1] = element;
            return updated;
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] updated = new T[source.Length - 1];

            int i = 0;
            int j = 0;

            while (i < source.Length)
            {
                if (i != index)
                {
                    updated[j] = source[i];
                    j++;
                }

                i++;
            }

            return updated;
        }
    }
}