using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace TestDllCaller
{
    class Program
    {
        static void Main(string[] args)
        {
            // Esempio base: tutto ciò che fa il programma chiamante è stampare a video il risultato della chiamata a funzione Messaggio
            Console.WriteLine(Messaggio("Hello world"));
        }

        // La funzione Messaggio riceve in input una stringa e la stampa corredata dai riferimenti DateTime relativi al momento
        // dell'esecuzione. In essa, in un punto desiderato, viene inserito il richiamo a FunzioneDaDll, che realizza l'esecuzione del metodo
        // richiesto presente nella DLL indicata
        private static string Messaggio(string v)
        {
            var m = "[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] " + v;

            FunzioneDaDLL("TestDLL", "Implementazione", m);

            // Dopo l'esecuzione del metodo della DLL, l'esecuzione continua come consueto
            return m;
        }

        // Dato un nomeDLL (eventualmente path completo), nome metodo/funzione da eseguire, e argomenti da passare, questa funzione
        // realizza l'invoke della stessa, eventualmente segnalando eccezione nel caso di DLL non presente, impossibilità ad eseguire, ecc.
        private static void FunzioneDaDLL(string nomeDLL, string metodo, string argomento)
        {
            try
            {
                // Viene caricata la DLL
                var a = Assembly.LoadFrom(nomeDLL + ".dll");

                // Si referenziano i suoi tipi (operazione che dà possibilità di accesso a quanto la DLL espone, ad esempio metodi)
                var t = a.GetTypes().FirstOrDefault();

                // Si referenzia il metodo desiderato
                var methodInfo = t.GetMethod(metodo);

                // Si istanzia la DLL con riferimento alla funzione desiderata
                var instance = Activator.CreateInstance(t, null);

                // Si invoca la funzione, passandole, se richiesto, un array di object contenente tutti i parametri di cui necessita,
                // ovviamente desumibili per numero e tipo dalla firma del metodo stesso (nel nostro caso, TestDLL.Funzioni.Implementazione)
                methodInfo.Invoke(instance, new object[] { argomento });

            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
