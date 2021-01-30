using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace daydream_click
{
    public class KeyboardImitateJob
    {
        
        private readonly List<Keys> _operates;

        private readonly int _interval;
        
        private  volatile bool  _checkStop = false;
        
        public KeyboardImitateJob(List<Keys> operates, int interval)
        {
            _operates = operates;
            _interval = interval;
        }

        public void Execute()
        {
            while (!_checkStop)
            {
                try
                {
                    foreach (var key in _operates)
                    {
                        // KeyboardSimulate.KeyboardPress(key);
                        Console.WriteLine("模拟按键按下："+key);
                    }
                    foreach (var key in _operates)
                    {
                        // KeyboardSimulate.KeyboardFreed(key);
                        Console.WriteLine("模拟按键弹起："+key);
                    }
                    Thread.Sleep(_interval);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void Stop()
        {
            _checkStop = true;
        }
        
    }
}