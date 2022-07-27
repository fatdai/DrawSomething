using System;
using static SDL2.SDL;

namespace HelloSDL
{
    class Log
    {
        public static void Info(string msg)
        {
            Console.WriteLine(msg);
        }
    }
    
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (SDL_Init(SDL_INIT_VIDEO) < 0)
            {
                Log.Info("init sdl error!");
                return;
            }
            Log.Info("init sdl success!");

            var window = SDL_CreateWindow("Hello", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 480, 320,
                0);
            if (window == IntPtr.Zero)
            {
                Log.Info("create window failed!");
                return;
            }

            SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "linear");

            var renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if (renderer == IntPtr.Zero)
            {
                Log.Info("Failed to create renderer");
                return;
            }
            
            // prepare scene
            SDL_SetRenderDrawColor(renderer, 96, 128, 255, 255);

            bool running = true;
           
            while (running)
            {
                // input
                if ( DoInput())
                {
                    running = false;
                    break;
                }
                
                // render
                SDL_RenderClear(renderer);
                
                // swap buffer
                SDL_RenderPresent(renderer);
                SDL_Delay(16);
            }
            
            // clean
        }
        
        static bool DoInput()
        {
            SDL_Event @event;
            while (SDL_PollEvent(out @event) != 0)
            {
                switch (@event.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        Log.Info("准备退出游戏!");
                        return true;
                    default:
                        break;
                }
            }

            return false;
        }
    }
    
    
   
}