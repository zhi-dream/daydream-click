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
                    // MouseSimulate.MouseMoveToPoint(new Point(_positionX,_positionY));
                    Console.WriteLine("模拟鼠标移动："+_positionX+_positionY);
                    switch (_operate)
                    {
                        case "左键":
                            // MouseSimulate.MouseSimulateLeftClick();
                            Console.WriteLine("模拟鼠标点击：左键");
                            break;
                        case "右键":
                            // MouseSimulate.MouseSimulateRightClick();
                            Console.WriteLine("模拟鼠标点击：右键");
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