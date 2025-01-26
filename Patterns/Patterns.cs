using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Collectionofpoems.Patterns
{
    public class Patterns
    {
        private Uml uml = new();
        Singleton singleObject = Singleton.Instance;

        public void Run()
        {
            //Console.WriteLine("Design Patterns");
            //uml.ShowRelations();
            //Console.WriteLine(singleObject.Hash("123"));
            //Type st = typeof(Singleton);
            //var ctr = st.GetConstructor(
            //     System.Reflection.BindingFlags.NonPublic |
            //     System.Reflection.BindingFlags.Instance,
            //     []);

            //Singleton secondObject = (Singleton)ctr.Invoke(new object[0]);
            //Console.WriteLine(secondObject.Hash("123"));
            //Console.WriteLine("{0} {1}",
            //    singleObject.GetHashCode(), secondObject.GetHashCode());

            //Singleton otherObject = Singleton.Instance;
            //Console.WriteLine("{0} {1}",
            //    singleObject.GetHashCode(), otherObject.GetHashCode());

            NotificationBuilder builder = new ();
                builder
                .SetIcon("The icon")
                .SetInput("The input");
            Notification notification=builder.build (); 
            Console.WriteLine (notification);

            Notification notification2=new NotificationBuilder()
                .SetIcon("The icon2")
                .SetInput("The input2")
                .SetCancellable("The cancellable2")
                .build();
            Console.WriteLine(notification2);



        }

        public class Uml
        {
            public void ShowRelations()
            {
                Console.WriteLine("RELATOIN");
                Console.WriteLine("Association: one-level interaction(App--Patterns)");
                Console.WriteLine("Aggregation: with object-reference  (Patterns-Singleton)");
                Console.WriteLine("Composition: with object-reference (Patterns-Uml)");
                Console.WriteLine("Dependency: direct relation ,parrameters or local varielbles (Program--App/Patterns)");
                Console.WriteLine("Generalization: (Interitance)");
            }

        }

        public class Singleton
        {
            public static Singleton Instance { get; private set; } = new();
            private Singleton()
            {
                if (Instance != null) throw new Exception("Singleton already created"); 
            }
            public String Hash(String input) => Convert.ToHexString(
                 System.Security.Cryptography.MD5.HashData(
                    System.Text.Encoding.UTF8.GetBytes(input)
                     )
                );
        }

        public class NotificationBuilder
        {
            private Notification notification = new();
            public NotificationBuilder SetTitle(String title)
            {
                notification.Title = title;
                return this;
            }
            public NotificationBuilder SetMessage(String message)
            {
                notification.Message = message;
                return this;
            }
            public NotificationBuilder SetIcon(String icon)
            {
                notification.Icon = icon;
                return this;
            }
            public NotificationBuilder SetImage(String image)
            {
                notification.Image = image;
                return this;
            }
            public NotificationBuilder SetPositiveButton(String positiveButton)
            {
                notification.PositiveButton = positiveButton;
                return this;
            }
            public NotificationBuilder SetNegativeButton(String negativeButton)
            {
                notification.NegativeButton = negativeButton;
                return this;
            }
            public NotificationBuilder SetNeutralButton(String neutralButton)
            {
                notification.NeutralButton = neutralButton;
                return this;
            }
            public NotificationBuilder SetInput(String input)
            {
                notification.Input = input; 
                return this;
            }
            public NotificationBuilder SetCancellable(String cancellable)
            {
                notification.Cancellable = cancellable;
                return this;
            }
            public Notification build()
            { 
                return notification;
            }

        }

         public class Notification
        {
            public String? Title, Message, Icon, Image, PositiveButton, NegativeButton,
                NeutralButton, Input, Cancellable;
            public override string ToString()
            {
                StringBuilder res = new();
                foreach (var field in typeof(Notification).GetFields())
                {
                    if(field.GetValue(this) is Object val)
                    {
                        res.Append(field.Name + "=" +val +" ");
                    }
                }

                return res.ToString();
            }
        }
    }
}
