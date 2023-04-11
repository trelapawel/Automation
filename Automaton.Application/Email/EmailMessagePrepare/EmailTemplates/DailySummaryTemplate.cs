namespace Automaton.Application.Email.EmailMessagePrepare.EmailTemplates
{
    public class DailySummaryTemplate : ITemplate
    {
        public override string ToString()
        {
            return @"
                    <html>
                        <head>
                            <style>
                    p {
                      font-family: Arial, Helvetica, sans-serif;
                    }
                    h1{
                        font-family :'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                        background-color: rgb(190, 164, 196);
                    }
                    h2{
                        font-family :'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                    }
                            </style>
                        </head>
                        <body align=""center"">
                             <p><h1>Dzienne podsumowanie</h1></p>
                                  <div>
                                      <p>Wszystkie zamówienia: $$AllPaidOrders$$</p>
                                      <p>Ten miesiąc: $$ThisMonthPaidOrders$$</p>
                                      <p>Poprzedni miesiąc: $$LastMonthPaidOrders$$</p>
                                      <p><h2>Suma $$Amount$$ zł</h2></p>
                                      <p><img width = ""250"" height = ""200"" src = ""https://static.wixstatic.com/media/ee3f9d_836db513cae9499c937dba6fda6956f2~mv2.gif""></p>
                                   </div>
                               </body>
                           </html>";

        }
    }
}
