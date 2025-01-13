// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

namespace TVRemoteProject
{
    class Program
    {
        // Televizyon kanalı sınıfı
        public class Channel
        {
            public int Number { get; set; }
            public string Name { get; set; }

            public Channel(int number, string name)
            {
                Number = number;
                Name = name;
            }
        }

        // Televizyon sınıfı
        public class Television
        {
            private List<Channel> channels;
            private int currentChannelIndex;

            public Television()
            {
                channels = new List<Channel>();
                currentChannelIndex = 0; // Başlangıç kanalı
            }

            public void AddChannel(Channel channel)
            {
                channels.Add(channel);
            }

            public void ShowCurrentChannel()
            {
                if (channels.Count == 0)
                {
                    Console.WriteLine("Hiç kanal bulunmuyor.");
                    return;
                }
                Console.WriteLine($"Şu anki Kanal: {channels[currentChannelIndex].Number} - {channels[currentChannelIndex].Name}");
            }

            public void NextChannel()
            {
                if (channels.Count == 0)
                {
                    Console.WriteLine("Hiç kanal bulunmuyor.");
                    return;
                }
                currentChannelIndex = (currentChannelIndex + 1) % channels.Count;
                ShowCurrentChannel();
            }

            public void PreviousChannel()
            {
                if (channels.Count == 0)
                {
                    Console.WriteLine("Hiç kanal bulunmuyor.");
                    return;
                }
                currentChannelIndex = (currentChannelIndex - 1 + channels.Count) % channels.Count;
                ShowCurrentChannel();
            }

            public void GoToChannel(int number)
            {
                for (int i = 0; i < channels.Count; i++)
                {
                    if (channels[i].Number == number)
                    {
                        currentChannelIndex = i;
                        ShowCurrentChannel();
                        return;
                    }
                }
                Console.WriteLine($"Kanal {number} bulunamadı.");
            }
        }

        static void Main(string[] args)
        {
            Television tv = new Television();

            // Örnek kanallar ekleme
            tv.AddChannel(new Channel(1, "TRT 1"));
            tv.AddChannel(new Channel(2, "Show TV"));
            tv.AddChannel(new Channel(3, "Star TV"));
            tv.AddChannel(new Channel(4, "Fox TV"));

            Console.WriteLine("TV Kumandası Uygulamasına Hoş Geldiniz!");
            Console.WriteLine("Kullanabileceğiniz komutlar: ileri, geri, git <kanal numarası>, çıkış");

            while (true)
            {
                Console.Write("Komut girin: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                string[] parts = input.Split(' ');
                string command = parts[0].ToLower();

                switch (command)
                {
                    case "ileri":
                        tv.NextChannel();
                        break;

                    case "geri":
                        tv.PreviousChannel();
                        break;

                    case "git":
                        if (parts.Length > 1 && int.TryParse(parts[1], out int channelNumber))
                        {
                            tv.GoToChannel(channelNumber);
                        }
                        else
                        {
                            Console.WriteLine("Geçerli bir kanal numarası girin.");
                        }
                        break;

                    case "çıkış":
                        Console.WriteLine("Uygulama sonlandırılıyor. İyi günler!");
                        return;

                    default:
                        Console.WriteLine("Geçersiz komut. Kullanabileceğiniz komutlar: ileri, geri, git <kanal numarası>, çıkış");
                        break;
                }
            }
        }
    }
}

