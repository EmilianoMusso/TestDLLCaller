using System;

namespace TestDLL
{
    public class Funzioni
    {
        // Costruttore vuoto per consentire la corretta individuazione del modulo da parte dei metodi Reflection del chiamante
        public Funzioni()
        {

        }

        // Esempio di funzione statica per invocazione nel chiamante
        // In questo caso, scriviamo un file di testo nel path eseguibile con stringa passata come argomento
        // Scopo di questa funzione è implementare, nel chiamante, l'esecuzione parametrizzata di una funzione
        public static void Implementazione(string valore)
        {
            System.IO.File.AppendAllText("testDLL.txt", valore + Environment.NewLine);
        }
    }
}
