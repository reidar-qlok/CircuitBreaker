/*
 I exemplet under används en CircuitBreaker-klass för att hantera anrop till en extern 
resurs genom Execute-metoden. Om antalet felaktiga anrop överstiger tröskelvärdet (failureThreshold), 
öppnas kretsen och anrop till den externa resursen undviks tills kretsen är stängd igen efter en viss 
tid (retryTimePeriod). Om kretsen är öppen, kastas ett undantag av typen CircuitBreakerOpenException, 
som kan fångas för att hantera felhantering när kretsen är öppen.
 */
namespace CircuitBreaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CircuitBreaker circuitBreaker = new CircuitBreaker(failureThreshold: 3, retryTimePeriod: 10);
            try
            {
                circuitBreaker.Execute(() =>
                {
                    // Anrop till extern resurs
                    // ...
                });
            }
            catch (CircuitBreakerOpenException ex)
            {
                // Hantera felhantering när kretsen är öppen
            }
            catch (Exception ex)
            {
                // Hantera andra typer av fel
            }
        }
    }
}