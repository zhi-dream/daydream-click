using System;
using System.Threading;

namespace daydream_click
{
    public class MouseImitateJob
    {
        
        private readonly string _operate;

        private readonly int _positionX;

        private readonly int _positionY;

        private readonly int _interval;
        
        private  volatile bool  _checkStop = false;
        
        public MouseImitateJob(string operate, int positionX, int positionY, int interval)
        {
            _operate = operate;
            _positionX = positionX;
            _positionY = positionY;
            _interval = interval;
        }

        public void Execute()
        {
            while (!_checkStop)
            {
                try
                {
                    Drive.MouseMove(_positionX,_positionY);
                    Console.WriteLine("模拟鼠标移动：X"+_positionX+"Y"+_positionY);
                    switch (_operate)
                    {
                        case "左键":
                            Drive.MouseLeftKeyClickDown();
                            Console.WriteLine("模拟鼠标左键：按下");
                            Thread.Sleep(new Random().Next(50,100));
                            Drive.MouseLeftKeyClickUp();
                            Console.WriteLine("模拟鼠标左键：弹起");
                            Thread.Sleep(new Random().Next(50,100));
                            break;
                        case "右键":
                            Drive.MouseRightKeyClickDown();
                            Console.WriteLine("模拟鼠标右键：按下");
                            Thread.Sleep(new Random().Next(50,100));
                            Drive.MouseRightKeyClickUp();
                            Console.WriteLine("模拟鼠标右键：弹起");
                            Thread.Sleep(new Random().Next(50,100));
                            break;
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