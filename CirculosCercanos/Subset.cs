using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CirculosCercanos
{
    public class Subset
    {
        private List<int> _members;

        Subset(int member)
        {
            _members = new List<int>();
            _members.Add(member);
        }
        
        public void Join(Subset y)
        {
            _members.AddRange(y._members);
        }

        public static int Find(Subset[] subsetsArray, int id)
        {
            for (int i = 0; i < subsetsArray.Length ;i++)
            {
                if (subsetsArray[i] != null)
                {
                    foreach (int member in subsetsArray[i]._members)
                    {
                        if (member == id)
                        {
                            return i;
                        }
                    }
                }
            }

            return -1;
        }

        public static Subset[] SubsetsForCollection(ICollection array)
        {
            Subset[] subsets = new Subset[array.Count];
            for (int i = 0; i < array.Count; i++)
            {
                subsets[i] = new Subset(i + 1);
            }

            return subsets;
        }
    }
}