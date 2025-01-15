using Collectionofpoems.ORM;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Text;


namespace Collectionofpoems.App

{
    internal class App
    {
        private List<Poem> poems = [];
        public void Run()
        {
            InitStore();
            Console.WriteLine("--------------------------");
            Join();
            Console.WriteLine("--------------------------");
            foreach (Poem poem in poems)
            {
                Console.WriteLine(poem);
                Console.WriteLine("-------------------------------------------------------------");
            }
            SaveStore();
            Delet();
            Console.WriteLine("--------------------------");
            foreach (Poem poem in poems)
            {
                Console.WriteLine(poem);
                Console.WriteLine("-------------------------------------------------------------");
            }
            LinqDemo();

            SaveStore();

            Console.WriteLine("-----------------------");
            LoadStore();

        }

        private void Join()
        {
            poems.Add(new Poem()
            {
                Name = Convert.ToString(Console.ReadLine()),
                Author= Convert.ToString(Console.ReadLine()),
                Year= Convert.ToInt32(Console.ReadLine()),
                Theme= Convert.ToString(Console.ReadLine()),
                Text= Convert.ToString(Console.ReadLine())

            });
        }
        private void Delet()
        {
            Console.WriteLine("Enter number poen for delete=>");
            int a =Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < poems.Count; i++) {
                if (i == (a - 1))
                {
                    poems.Remove(poems[i]);
                }
            }
        }

      private void Srt()
        {
            poems.Sort();
        }

        private void LoadStore()
        {
            String dirName = GetWorkDirectory();
            // Збираємо всі файли, які мають ім'я "store...."
            DirectoryInfo dir = new(dirName);
            // готуємо колекцію
            List<FileInfo> files = new();
            // Перебираємо усі файли, збираємо до колекції ті, що проходять перевірку
            //  - ім'я починається з "store"
            //  - розширення або ".json" або ".xml"
            foreach (FileInfo fileInfo in dir.GetFiles())
            {
                if (fileInfo.Name.StartsWith("Wings"))
                {
                    files.Add(fileInfo);
                }
            }
            // Те ж саме, тільки LINQ
            List<FileInfo> filesLinq =
                dir
                .GetFiles()
                .Where(f => f.Name.StartsWith("Wings"))
                .ToList();
            Console.WriteLine("Який файл використати?");
            for (int i = 0; i < filesLinq.Count; i++)
            {
                Console.WriteLine("{0} {1}", i + 1, filesLinq[i]);
            }
            int choice = Convert.ToInt32(Console.ReadLine()) - 1;
            // TODO: перевірити на валідність
            // 
            // Десеріалізуємо
            // визначаємо розширення файлу
            if (filesLinq[choice].Extension == ".json")
            {
                // другий спосіб роботи з файлами - заготовлені у мові інструменти
                String fileContent = File.ReadAllText(filesLinq[choice].FullName);

                poems = JsonSerializer.Deserialize<List<Poem>>(fileContent)!;
            }
        }

        private String GetWorkDirectory()
        {
            /* Регулярні вирази - мова шаблонів рядків
             *  - пошук у т.ч. перевірка формату
             *  - заміна
             *  - поділ / розбиття
             * Особливість - використання спеціальних конструкцій
             *  - групових символів на кшталт 
             *     \s - space (пробіл, табуляція, розриви тощо)
             *     \w - word-symbol (те, що може бути в іменах змінних)
             *     \d - digit
             *     \D - non-digit
             *     [abc] - a або b або с
             *  - квантифікаторів (покажчиків кількості)
             *     + - 1 і більше
             *     * - 0 і більше
             *     ? - 0 чи 1
             *     {2,5} - від 2 до 5
             */

            String dirName = Directory.GetCurrentDirectory();
            Regex pattern = new(@"[/\\][Dd]ebug[/\\]");
            if (pattern.IsMatch(dirName))
            {
                dirName = Regex.Replace(dirName, @"[/\\][Bb]in[/\\].*", "");
            }
            return dirName;
        }

        private void LinqDemo()
        {
            foreach (var poem in poems.Where(b => b.Year > 1845))
            {
                Console.WriteLine(poem);
            }

        }


        private String SerializeStoreJson()
        {
            return JsonSerializer.Serialize(poems);
        }

        private void SaveStore()
        {
            String dirName = GetWorkDirectory();
            using FileStream fileStream = new($"{dirName}/collectionOFpoems.json", FileMode.Create);
            byte[] code = System.Text.Encoding.UTF8.GetBytes(SerializeStoreJson());
            fileStream.Write(code, 0, code.Length);
        }

        public void InitStore()
        {
            poems.Add(new Poem()
            {
                Name = "Zapovit",
                Author = "Taras Shevchenko",
                Year = 1845,
                Theme = "politics",
                Text = "When I am dead, bury me\r\nIn my beloved Ukraine,\r\nMy tomb upon a grave mound high\r\nAmid the spreading plain,\r\nSo that the fields, the boundless steppes,\r\nThe Dnieper's plunging shore\r\nMy eyes could see, my ears could hear\r\nThe mighty river roar."

            });
            poems.Add(new Poem()
            {
                Name = "Contra spem spero",
                Author = "Lesya Ukrainka",
                Year = 1910,
                Theme = "politics",
                Text = "For now springtime comes, agleam with gold!\r\nShall thus in grief and wailing for ill-fortune\r\nAll the tale of my young years be told?\r\n\r\nNo, I want to smile through tears and weeping.,\r\nSing my songs where evil holds its sway,\r\nHopeless, a steadfast hope forever keeping,\r\nI want to live! You thoughts of grief, away!"
            });
            poems.Add(new Poem()
            {
                Name = "Wings",
                Author = "Lina Kostenko",
                Year = 1879,
                Theme = "nature",
                Text = "He lives on the ground. I do not fly.\r\nA wing has. A wing has!\r\n\r\nThey are the wings, not down, now, \"I\r\nAnd of truth, virtue and trust \"me.                                                                                  Ліна Костенко (1930)\r\n\r\nWho - with fidelity in love.\r\nWho - with eternal aspirations.\r\n\r\nWho - with sincerity to work.\r\nWho - with generosity to care.\r\n\r\nWho - the song, or hope,\r\nOr with poetry, or dreams."
            });
            poems.Add(new Poem()
            {
                Name = "The Mighty Dnieper",
                Author = "Taras Shevchenko",
                Year = 1852,
                Theme = "nature",
                Text = "The mighty Dnieper roars and bellows,\r\nThe wind in anger howls and raves,\r\nDown to the ground it bends the willows,\r\nAnd mountain-high lifts up the waves.\r\nThe pale-faced moon picked out this moment\r\nTo peek out from behind a cloud,\r\nLike a canoe upon the ocean\r\nIt first tips up, and then dips down.\r\nThe cocks don't crow to wake the morning,\r\nThere's not as yet a sound of man,\r\nThe owls in glades call out their warnings,\r\nAnd ash trees creak and creak again."
            });

        }

    }


}