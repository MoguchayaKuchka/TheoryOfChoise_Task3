using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheoryOfChoise_Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            int[] prices = new int[30];
            int[] duration = new int[30];
            int[] hotel_qual = new int[30];
            string[] table = new string[30];

            for(int i = 0;i<30;i++)
            {
                prices[i] = rnd.Next(90000, 300000);
                duration[i] = rnd.Next(7, 32);
                hotel_qual[i] = rnd.Next(1, 11);
            }

            Console.WriteLine("№ |Price |Duration|Quality|");
            Console.WriteLine("---------------------------");
            for (int i = 0; i < 30; i++)
            {
                string output = "";
                if (Convert.ToString(i+1).Length == 1) output += Convert.ToString(i+1) + " |";
                else output += Convert.ToString(i+1)+"|";
                if (Convert.ToString(prices[i]).Length == 5) output += Convert.ToString(prices[i]) + " |";
                else output += Convert.ToString(prices[i])+ "|";
                if (Convert.ToString(duration[i]).Length == 1) output += Convert.ToString(duration[i]) + "       |";
                else output += Convert.ToString(duration[i])+"      |";
                if (Convert.ToString(hotel_qual[i]).Length == 1) output += Convert.ToString(hotel_qual[i]) + "      |";
                else output += Convert.ToString(hotel_qual[i]) + "     |";
                table[i] = output;
                Console.WriteLine(output);
                Console.WriteLine("---------------------------");
            }

            Console.WriteLine("\nPresets:\nPrice - min 175 000. Max 300 000\nDuration - min 16 day. Max 31 day\nQuality - min 5. Max 10\n");
            Console.WriteLine("№ |Price |Duration|Quality|");
            Console.WriteLine("---------------------------");
            List<int> alternatives = new List<int>();
            
            string first_answ = "\nПри указании нижних границ возможные альтернативы: ";
            for(int i =0;i<30;i++)
            {
                if (prices[i] > 175000 && duration[i] > 16 && hotel_qual[i] > 5)
                {
                    Console.WriteLine(table[i] + " - Fits");
                    first_answ += i + 1 + ", ";
                    alternatives.Add(i);
                }
                else Console.WriteLine(table[i]);
                Console.WriteLine("---------------------------");
            }
            if (alternatives.Count == 0) Console.WriteLine("Нет подходящих альтернатив");
            else Console.WriteLine(first_answ);
            string second_answ = "Множество Парето: ";
            int[] test_prices = prices;
            int[] test_duration = duration;
            int[] test_qual = hotel_qual;
            for (int first_index = 0; first_index < 30; first_index++)
            {
                for (int second_index = 0; second_index < 30; second_index++)
                {

                    if (test_prices[first_index] == 0) break;
                    
                    if ((test_prices[first_index] > test_prices[second_index] && test_duration[first_index] > test_duration[second_index] && test_qual[first_index] > test_qual[second_index]) ||
                        (test_prices[first_index] > test_prices[second_index] && test_duration[first_index] == test_duration[second_index] && test_qual[first_index] == test_qual[second_index]) ||
                        (test_prices[first_index] == test_prices[second_index] && test_duration[first_index] > test_duration[second_index] && test_qual[first_index] == test_qual[second_index]) ||
                        (test_prices[first_index] == test_prices[second_index] && test_duration[first_index] == test_duration[second_index] && test_qual[first_index] > test_qual[second_index]) ||
                        (test_prices[first_index] > test_prices[second_index] && test_duration[first_index] > test_duration[second_index] && test_qual[first_index] == test_qual[second_index]) ||
                        (test_prices[first_index] == test_prices[second_index] && test_duration[first_index] > test_duration[second_index] && test_qual[first_index] > test_qual[second_index]) ||
                        (test_prices[first_index] > test_prices[second_index] && test_duration[first_index] == test_duration[second_index] && test_qual[first_index] > test_qual[second_index]))
                    {
                        test_prices[second_index] = 0;
                        test_duration[second_index] = 0;
                        test_qual[second_index] = 0;
                    }
                    else if ((test_prices[first_index] < test_prices[second_index] && test_duration[first_index] < test_duration[second_index] && test_qual[first_index] < test_qual[second_index]) ||
                        (test_prices[first_index] < test_prices[second_index] && test_duration[first_index] == test_duration[second_index] && test_qual[first_index] == test_qual[second_index]) ||
                        (test_prices[first_index] == test_prices[second_index] && test_duration[first_index] < test_duration[second_index] && test_qual[first_index] == test_qual[second_index]) ||
                        (test_prices[first_index] == test_prices[second_index] && test_duration[first_index] == test_duration[second_index] && test_qual[first_index] < test_qual[second_index]) ||
                        (test_prices[first_index] < test_prices[second_index] && test_duration[first_index] < test_duration[second_index] && test_qual[first_index] == test_qual[second_index]) ||
                        (test_prices[first_index] == test_prices[second_index] && test_duration[first_index] < test_duration[second_index] && test_qual[first_index] < test_qual[second_index]) ||
                        (test_prices[first_index] < test_prices[second_index] && test_duration[first_index] == test_duration[second_index] && test_qual[first_index] < test_qual[second_index]))
                    {
                        test_prices[first_index] = 0;
                        test_duration[first_index] = 0;
                        test_qual[first_index] = 0;
                    }
                }
            }
            for(int i=0;i<30;i++)
            {
                if (test_prices[i] != 0) second_answ += (i + 1) + ", ";
            }
            Console.WriteLine(second_answ);
            string third_answ = "При субоптимизации альтернативы:";
            
            Console.WriteLine("\nPresets:\nPrice - min 175 000. Max 300 000\nDuration - MAIN CRITERIA. Max 31 day\nQuality - min 5. Max 10\n");
            
            if (alternatives.Count == 1)
            {
                third_answ += alternatives[0] + 1;
                
            }
            else
            {
                int maxim = 0;
                for (int i = 1; i < alternatives.Count; i++)
                {
                    if (duration[alternatives[i]] > duration[alternatives[maxim]]) maxim = i;
                }
                third_answ += Convert.ToString(alternatives[maxim] + 1) + ", ";
                
                for (int i = 0; i < alternatives.Count; i++)
                {
                    if (duration[alternatives[i]] == duration[alternatives[maxim]] && i != maxim)
                    {
                        third_answ += Convert.ToString(alternatives[i] + 1) + ", ";
                        
                    }
                }
                

            }
            
            Console.WriteLine(third_answ);
            Console.WriteLine("\nPresets:\nDuration - FIRST. Max 31 day\nQuality - SECOND. Max 10\nPrice - THIRD. Max 300 000\n");
            List<int> dur_alts = new List<int>();
            string fourth_answ = "При лексикографической оптимизации альтернативы: ";
            int max = 0;
            for(int i = 1;i<30;i++)
            {
                if (duration[i] > duration[max]) max = i;
            }
            dur_alts.Add(max);
            for(int i = 0;i<30;i++)
            {
                if (duration[i] == duration[max] && i != max) dur_alts.Add(i);
            }
            if (dur_alts.Count == 1) Console.WriteLine(fourth_answ + (dur_alts[0]+1));
            else
            {
                List<int> qual_alts = new List<int>();
                max = 0;
                for(int i =1;i<dur_alts.Count;i++)
                {
                    if(hotel_qual[dur_alts[i]]>hotel_qual[dur_alts[max]]) max = i;
                }
                qual_alts.Add(dur_alts[max]);
                for(int i = 0;i<dur_alts.Count;i++)
                {
                    if(hotel_qual[dur_alts[i]]==hotel_qual[dur_alts[max]]&&i!=max) qual_alts.Add(dur_alts[i]);
                }
                if(qual_alts.Count == 1) Console.WriteLine(fourth_answ+(qual_alts[0]+1));
                else
                {
                    List<int> price_alts = new List<int>();
                    max = 0;
                    for (int i = 1; i < qual_alts.Count; i++)
                    {
                        if (prices[hotel_qual[dur_alts[i]]] > prices[hotel_qual[dur_alts[max]]]) max = i;
                    }
                    price_alts.Add(prices[hotel_qual[dur_alts[max]]]);
                    for (int i = 0; i < qual_alts.Count; i++)
                    {
                        if (prices[hotel_qual[dur_alts[i]]] == prices[hotel_qual[dur_alts[max]]] && i != max) price_alts.Add(prices[hotel_qual[dur_alts[i]]]);
                    }
                    if (price_alts.Count == 1) Console.WriteLine(fourth_answ + (price_alts[0]+1));
                    else
                    {
                        for(int i = 0;i<price_alts.Count;i++)
                        {
                            fourth_answ+=price_alts[i]+", ";
                        }
                        Console.WriteLine(fourth_answ);
                    }
                }
            }
            string fivth_answ = "При построении обобщённого критерия альтернативы: ";
            double[] criterias = new double[30];
            Console.WriteLine("\nPresets:\nDuration - 0,5 weight.Max 31 day\nQuality - 0,3 weight.Max 10\nPrice - 0,2 weight.Max 300 000\n");
            Console.WriteLine("№ |Price |Duration|Quality|");
            Console.WriteLine("---------------------------");
            for (int i=0;i<30;i++)
            {
                criterias[i] = (double)duration[i] / 31 * 0.5 + (double)hotel_qual[i]/10*0.3+(double)prices[i]/300000*0.2;
                Console.WriteLine(table[i] + " " + criterias[i]);
            }
            max = 0;
            for(int i = 0;i<30;i++)
            {
                if(criterias[i] > criterias[max]) max = i;
            }
            Console.WriteLine(fivth_answ + (max + 1));
            Console.ReadKey();


        }
    }
}
