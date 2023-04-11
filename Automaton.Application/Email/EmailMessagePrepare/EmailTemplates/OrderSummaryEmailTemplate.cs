namespace Automaton.Application.Email.EmailMessagePrepare.EmailTemplates
{
    public class OrderSummaryEmailTemplate : ITemplate
    {
        public override string ToString()
        {
            return @"<html>
                        <head>
                            <style>
                                p {
                                  font-family: Arial, Helvetica, sans-serif;
                                }
                                h1{
                                    font-family :'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                    background-color: greenyellow;
                                }
                            </style>
                        </head>
                        <body align=""center"">
                            <p> Dzień dobry </ p >
                               <p> Dziękuję za zakupienie produktu: $$ProductName$$</ p >
                                 <div>
                                    <table align = ""center"" >
                                        <tr>
                                            <td align = ""center"">Numer zamówienia:</td>
                                        </tr>
                                        <tr>
                                            <td align = ""center""><h1 style = ""text-shadow: rgb(182, 255, 109);"" >$$OrderNo$$</4h1 ></td>
                                        </tr>
                                    </table>
                                    </div>
                                        <p>Proszę o przesłanie kwoty $$Price$$ zł na konto o numerze: </br>$$AcountNo$$
                                        <br><strong>W tytule przelewu proszę wpisać nr zamówienia czyli $$OrderNo$$</strong>
                                        <br>Po zaksięgowaniu wpłaty zakupiony produkt zostanie przesłany na podany adres email.</p>
                                        <p> Pozdrawiam </ br > Iguana do Wycinania </p>
                                        <p><img src = ""https://static.wixstatic.com/media/ee3f9d_7870b6387db944f9bda351d13b018dee~mv2.jpg/v1/fill/w_157,h_157,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/Iguana_do_wycinania_Logo.jpg""></p>
                        </body>
                    </html> "
            ;
        }
    }
}
