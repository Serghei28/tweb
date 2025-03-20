using System;

namespace YourProject.BusinessLogic.API

{
    public class BaseAPI
    {
        protected readonly string SupabaseUrl;
        protected readonly string SupabaseAnonKey;

        public BaseAPI()
        {
            // Подгружаем данные из переменных окружения (если они нужны)
            SupabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL") ?? "your_supabase_url";
            SupabaseAnonKey = Environment.GetEnvironmentVariable("SUPABASE_ANON_KEY") ?? "your_supabase_anon_key";
        }
    }
}
