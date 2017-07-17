using BlackSound.Tools;
using DataAccess;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Views
{
    abstract class BaseView<T> where T : class, IBaseEntity, new()
    {
        //base CRUD
        public List<ViewItem> viewItems = new List<ViewItem>();

        public BaseView()
        {
            Type type = typeof(T);
            viewItems.Add(new ViewItem("C", $"[C]reate a {type.Name}", Add));
            viewItems.Add(new ViewItem("V", $"[V]iew a {type.Name}", View));
            viewItems.Add(new ViewItem("U", $"[U]pdate a {type.Name}", Update));
            viewItems.Add(new ViewItem("D", $"[D]elete a {type.Name}", Delete));
        }

        protected abstract BaseRepository<T> CreateRepository();
        protected abstract void RenderItem(T item);
        protected abstract void PopulateItem(T item);
        protected virtual void AddMoreLogic(T item) { }
        protected virtual void DeleteMoreLogic(int id) { } //cascade delete
        protected virtual void UpdateItem(T itemDb) { }
        protected virtual List<T> GetListUpdate(BaseRepository<T> baseRepo) { return null; }
        protected virtual List<T> GetListView(BaseRepository<T> baseRepo) { return null; }
        protected virtual void ViewMoreLogic(int id) { }
        protected abstract void RenderShortInfo(T item);

        public void Show()
        {
            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();
                if (choice.ToUpper() == "X")
                {
                    break;
                }

                FindProperAction(choice);
            }
        }

        public void DisplayMenu()
        {
            Console.Clear();
            Type type = typeof(T);
            Console.WriteLine("{0}s Management", type.Name);
            viewItems.ForEach(item => Console.WriteLine(item.Text));
            Console.WriteLine("E[x]it");
        }

        private void FindProperAction(string choice)
        {
            ViewItem viewItem = viewItems.FirstOrDefault(item => item.KeyPressed == choice.ToUpper());
            if (viewItem != null)
            {
                viewItem.ActionMethod();
            }
            else
            {
                Console.WriteLine("Invalid choice");
                Console.ReadKey(true);
                return;
            }
        }

        public void View()
        {
            Console.Clear();
            BaseRepository<T> baseRepo = CreateRepository();
            List<T> list = GetListView(baseRepo);
            Type type = typeof(T);
            if (list.Count == 0)
            {
                Console.WriteLine("No {0}s found", type.Name);
                Console.ReadKey(true);
                return;
            }
            foreach (T item in list)
            {
                RenderShortInfo(item);
            }
            Console.Write("{0} ID: ", type.Name);
            int idInput = Int32.Parse(Console.ReadLine());
            Console.Clear();
            if (!list.Any(item => item.Id == idInput))
            {
                Console.WriteLine("Cannot find {0}", type.Name);
                Console.ReadKey(true);
                return;
            }
            T itemDb = list.Where(item => item.Id == idInput).FirstOrDefault();
            RenderItem(itemDb);
            ViewMoreLogic(itemDb.Id);
            Console.ReadKey(true);
        }

        protected void Update()
        {
            Console.Clear();
            Type type = typeof(T);
            BaseRepository<T> baseRepo = CreateRepository();
            List<T> updateList = GetListUpdate(baseRepo);
            foreach (T item in updateList)
            {
                RenderItem(item);
            }
            Console.WriteLine("Update a {0}", type.Name);
            Console.Write("Enter {0} ID: ", type.Name);
            int inputId = Int32.Parse(Console.ReadLine());
            if (!updateList.Any(playlist => playlist.Id == inputId))
            {
                Console.WriteLine("{0} not found!", type.Name);
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            T itemDb = updateList.FirstOrDefault(x => x.Id == inputId);
            UpdateItem(itemDb);
            baseRepo.Save(itemDb);
            Console.WriteLine("{0} saved successfully!", type.Name);
            Console.ReadKey(true);
        }

        protected void Delete()
        {
            Console.Clear();
            Type type = typeof(T);
            BaseRepository<T> baseRepo = CreateRepository();
            List<T> deleteList = GetListUpdate(baseRepo);
            if (deleteList.Count == 0)
            {
                Console.WriteLine("There aren't any {0}s", type.Name);
                Console.ReadKey(true);
                return;
            }
            foreach (T item in deleteList)
            {
                RenderItem(item);
            }
            Console.WriteLine("Delete a {0}", type.Name);
            Console.Write("{0} id: ", type.Name);
            int idInput = Int32.Parse(Console.ReadLine());
            Console.Clear();
            if (!deleteList.Any(x => x.Id == idInput))
            {
                Console.WriteLine("Cannot find {0}", type.Name);
                Console.ReadKey(true);
                return;
            }
            DeleteMoreLogic(idInput);
            baseRepo.Delete(x => x.Id == idInput);
            Console.WriteLine("{0} deleted successfully!", type.Name);
            Console.ReadKey(true);
        }


        protected void Add()
        {
            Console.Clear();
            BaseRepository<T> baseRepo = CreateRepository();
            T item = new T();
            Type type = typeof(T);
            Console.WriteLine("Create a new {0}", type.Name);
            PopulateItem(item);
            baseRepo.Save(item);
            AddMoreLogic(item);
            Console.WriteLine("{0} saved successfully!", type.Name);
            Console.ReadKey(true);
        }
    }
}
