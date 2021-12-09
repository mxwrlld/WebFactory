using System;
using System.Collections.Generic;
using System.Text;
using _2._1.Exceptions;

namespace _2._1
{
    class Tank
    {
        public double MaxVoLume { get; }

        public double CurrentVolume { get; private set; } = 0;

        public event Action<string> Notify;

        public Tank(double maxVoLume)
        {
            MaxVoLume = maxVoLume;
        }

        public void Add(double volume)
        {
            if (CurrentVolume + volume <= MaxVoLume)
            {
                CurrentVolume += volume;
                Notify?.Invoke($"Попытка долить {volume} л. Статус: Успешно");
            }
            else
            {
                Notify?.Invoke($"Попытка долить {volume} л. Статус: Отказано \nПричина: ");
                throw new TankOverflowException("Цистерна переполнена!");
            }

            Notify?.Invoke($"Заполнено {CurrentVolume} л. из {MaxVoLume} л.");
        }
    
        public void Take(double volume)
        {
            if(CurrentVolume - volume >= 0)
            {
                CurrentVolume -= volume;
                Notify?.Invoke($"Попытка слить {volume} л. Статус: Успешно");
            }
            else
            {
                Notify?.Invoke($"Попытка слить {volume} л. Статус: Отказано \nПричина: ");
                throw new NotEnoughException("Минимальный объём цистерны - 0 литров!");
            }

            Notify?.Invoke($"Заполнено {CurrentVolume} л. из {MaxVoLume} л.");
        }
    }
}
