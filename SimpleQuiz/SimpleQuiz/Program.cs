using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SimpleQuiz
{
    [Serializable()]
    class Program
    {
        public static List<Frage> fragen = new List<Frage>()
        {
            new Frage("Wessen Genesung schnell voranschreitet, der erholt sich...?",
                                      "hinguckends",
                                      "anschauends",
                                      "zusehends",
                                      "glotzends",
                                      "3"),
            new Frage("Natürlich spielten musikalische Menschen auch im...?",
                                      "Südpo Saune",
                                      "Ostblock Flöte",
                                      "Westsaxo Phon",
                                      "Nordklari Nette",
                                      "2"),
            new Frage("Wobei gibt es keine geregelten Öffnungszeiten?",
                                     "Baumärkte",
                                     "Möbelhäuser",
                                     "Teppichgeschäfte",
                                     "Fensterläden",
                                     "4")
        };

        Boolean addQuestion = true;

        static void Main(string[] args)
        {
            Program program = new Program();

            program.CheckFileExists();
            program.FragenLaden();

            Console.WriteLine("\nSoll eine Frage ergänzt werden?");
            program.AddQuestion();

            while (program.addQuestion == true)
            {
                Console.WriteLine("Weitere Frage hinzufügen?");
                program.AddQuestion();
            }

            program.RemoveQuestion();

            program.Ausgabe();

            program.FragenSpeichern();

            Console.ReadKey();
        }



        public void Ausgabe()
        {
            int id = 0;
            foreach (var value in fragen)
            {
                Console.WriteLine("Frage und Antworten" + "ID " + id + " | " + value.frage + " | "
                                                                                        + value.antwort1 + " | "
                                                                                        + value.antwort2 + " | "
                                                                                        + value.antwort3 + " | "
                                                                                        + value.antwort4 + " | "
                                                                                        + value.richtigeAntwort);

                id = id + 1;
            }
        }

        public void FragenLaden()
        {
            FileStream fs = new FileStream(@"FragenListe.dat", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            fragen = (List<Frage>)formatter.Deserialize(fs);
            fs.Close();
        }

        public void FragenSpeichern()
        {
            FileStream stream;
            stream = new FileStream(@"FragenListe.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, fragen);
            stream.Close();
        }

        public void CheckFileExists()
        {
            string check;
            string checkfile = @"FragenListe.dat";
            check = File.Exists(checkfile) ? "ja" : "nein";


            if (check.Equals("nein"))
            {
                Console.WriteLine("Da noch keine Fragendatenbank vorhanden ist, wird eine neue mit 3 Standard Fragen erstellt\n");
                FragenSpeichern();
            }
            else
            {
                Console.WriteLine("Es wird eine vorhandene Fragendatenbank verwendet");
            }
        }




        public void RemoveQuestion()
        {
            Console.WriteLine("\nSoll eine Frage gelöscht werden?");

            if (Console.ReadLine().Equals("j"))
            {
                Console.WriteLine("\nBitte den Index der zu löschenden Frage eingeben!");
                int foo = Int32.Parse(Console.ReadLine());

                fragen.RemoveAt(foo);

                Console.WriteLine("\nSoll eine weitere Frage gelöscht werden?");
                if (Console.ReadLine().Equals("j"))
                {
                    RemoveQuestion();
                }
            }
            else
            {
                Console.WriteLine("Alle Fragen bleiben erhalten!");
            }
        }

        public void AddQuestion()
        {
            String questionTemp = "";
            String answer1Temp = "";
            String answer2Temp = "";
            String answer3Temp = "";
            String answer4Temp = "";
            String rightAnswerTemp = "";

            if (Console.ReadLine().Equals("j"))
            {
                Console.WriteLine("\nBitte Frage eingeben!");
                questionTemp = Console.ReadLine();

                Console.WriteLine("Bitte 1. Antwort eingeben!");
                answer1Temp = Console.ReadLine();
                Console.WriteLine("Bitte 2. Antwort eingeben!");
                answer2Temp = Console.ReadLine();
                Console.WriteLine("Bitte 3. Antwort eingeben!");
                answer3Temp = Console.ReadLine();
                Console.WriteLine("Bitte 4. Antwort eingeben!");
                answer4Temp = Console.ReadLine();
                Console.WriteLine("Bitte richtige Antwort eingeben!");
                rightAnswerTemp = Console.ReadLine();

                fragen.Add(new Frage(questionTemp, answer1Temp, answer2Temp, answer3Temp, answer4Temp, rightAnswerTemp));
            }
            else
            {
                addQuestion = false;
            }
        }

        [Serializable()]
        public class Frage : Program
        {
            public String frage;
            public String antwort1;
            public String antwort2;
            public String antwort3;
            public String antwort4;
            public String richtigeAntwort;


            public Frage(String question, String answer1, String answer2, String answer3, String answer4, String rightAnswer)
            {
                frage = question;
                antwort1 = answer1;
                antwort2 = answer2;
                antwort3 = answer3;
                antwort4 = answer4;
                richtigeAntwort = rightAnswer;

                Console.WriteLine("\nDie Frage lautet:\n");
                Console.WriteLine(frage);
                Console.WriteLine("1. : " + antwort1);
                Console.WriteLine("2. : " + antwort2);
                Console.WriteLine("3. : " + antwort3);
                Console.WriteLine("4. : " + antwort4);

                EditQuestion();
                EditAnswers();
                PrintNewQuestionAnswer();
                pruefeAntwort(NewRightAnswer(richtigeAntwort));
            }

            public void EditQuestion()
            {
                Console.WriteLine("\nBestehende Frage ändern?");

                if (Console.ReadLine().Equals("j"))
                {
                    Console.WriteLine("Bitte neue Frage eingeben!");
                    frage = Console.ReadLine();
                }
                else
                {
                }
            }

            public void EditAnswers()
            {
                Console.WriteLine("\nAntwort(en) ändern?");

                if (Console.ReadLine().Equals("j"))
                {
                    Console.WriteLine("1. Antwort ändern?");
                    if (Console.ReadLine().Equals("j"))
                    {
                        Console.WriteLine("Bitte neue Antwort eingeben!");
                        antwort1 = Console.ReadLine();
                        Console.WriteLine("Die erste Antwort lautet nun: " + antwort1);
                    }
                    Console.WriteLine("2. Antwort ändern?");
                    if (Console.ReadLine().Equals("j"))
                    {
                        Console.WriteLine("Bitte neue Antwort eingeben!");
                        antwort2 = Console.ReadLine();
                        Console.WriteLine("Die zweite Antwort lautet nun: " + antwort2);
                    }
                    Console.WriteLine("3. Antwort ändern?");
                    if (Console.ReadLine().Equals("j"))
                    {
                        Console.WriteLine("Bitte neue Antwort eingeben!");
                        antwort3 = Console.ReadLine();
                        Console.WriteLine("Die dritte Antwort lautet nun: " + antwort3);
                    }
                    Console.WriteLine("4. Antwort ändern?");
                    if (Console.ReadLine().Equals("j"))
                    {
                        Console.WriteLine("Bitte neue Antwort eingeben!");
                        antwort4 = Console.ReadLine();
                        Console.WriteLine("Die vierte Antwort lautet nun: " + antwort4);
                    }
                }
            }
            public void PrintNewQuestionAnswer()
            {
                Console.WriteLine("\n\nDie aktualisierte Frage mit Antworten lautet:\n");
                Console.WriteLine(frage);
                Console.WriteLine("1. : " + antwort1);
                Console.WriteLine("2. : " + antwort2);
                Console.WriteLine("3. : " + antwort3);
                Console.WriteLine("4. : " + antwort4);
            }

            public String pruefeAntwort(String answer)
            {
                Console.WriteLine("Wie war nochmal die Antwort?");
                if (!Console.ReadLine().Equals(answer))
                {
                    Console.WriteLine("Ihre Antwort war falsch!\n");
                }
                else
                {
                    Console.WriteLine("Ihre Antwort war korrekt!\n");
                }
                return answer;
            }

            public String NewRightAnswer(String answer)
            {
                String neueRichtigeAntwort = answer;
                Console.WriteLine("\nMöchten Sie die richtige Antwort ändern?");

                if (Console.ReadLine().Equals("j"))
                {
                    Console.WriteLine("Bitte neue richtige Antwort eingeben!");
                    neueRichtigeAntwort = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Antwort bleibt bestehen!");
                }
                return neueRichtigeAntwort;
            }
        }
    }
}


