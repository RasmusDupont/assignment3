using System;
using System.Collections.Generic;


namespace questionAserver
{
    public class Categories
    {
        private static List<Category> categories;

        public Categories()
        {
            categories = new List<Category>();

            categories.Add(new Category(1, "Beverages"));
            categories.Add(new Category(2, "Condiments"));
            categories.Add(new Category(3, "Confections"));
        }

        public Category GetCategory(int id)
        {

            for (int i = 0; i < categories.Count; i++)
            {
                if(id == categories[i].Id)
                {
                    return categories[i];
                }
            }
            return null;
        }

    }
}
