using System;
using System.Collections.Generic;


namespace questionAserver
{
    public class CategoryList
    {
        private CategoryList() { }
        
        private  static List<Category> myList = new List<Category>();

        public static List<Category> Getlist()
        { return myList; }

        public static Category GetCategory(int id)
        {

            for (int i = 0; i < myList.Count; i++)
            {
                if (id == myList[i].Id)
                {
                    return myList[i];
                }
            }
            return null;
        }
        public static void Updatelist(int id, string name)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                if (id == myList[i].Id)
                {
                    myList[i].Name = name;
                }
            }
        }
        public static void Updatelist(int id, string name, int newid)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                if (id == myList[i].Id)
                {
                    myList[i].Name = name;
                    myList[i].Id = newid;
                }
            }
        }
        public static void Updatelist(int id, int newid)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                if (id == myList[i].Id)
                {

                    myList[i].Id = newid;
                }
            }
        }

    }
}
