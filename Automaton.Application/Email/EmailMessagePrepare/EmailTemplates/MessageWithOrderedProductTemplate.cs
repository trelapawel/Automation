namespace Automaton.Application.Email.EmailMessagePrepare.EmailTemplates
{
    public class MessageWithOrderedProductTemplate : ITemplate
    {
        public override string ToString()
        {
            return @"<html>
                        <head>
                            <style>
                                p {
                                    font - family: Arial, Helvetica, sans - serif;
                                    }
                                h1{
                                    font - family :'Franklin Gothic Medium', 'Arial Narrow', Arial, sans - serif;
                                    background - color: greenyellow;
                                    }
                            </style>
                        </head>
                    <body align = ""center"">
                        <p>Dzień dobry </p>
                        <p>Twoje zamówienie: $$ProductName$$
                        </br>o numerze $$OrderNo$$
                        </br>Zostało prawidłowo opłacone. Aby pobrać plik kliknij w poniższy przycisk.
                        </br>
                        </p>
                        </br>
                        <a href = ""$$LinkToProduct$$"">
                            <img src = ""https://static.wixstatic.com/media/ee3f9d_0b29b9d10a47418b8bc2d0eba1c904a6~mv2.png/v1/fill/w_363,h_107,al_c,q_85,usm_0.66_1.00_0.01,enc_auto/pobierz_pdf.png""
                                alt=""[Pobierz pdf]"">
                         </a>
                         </br>
                         </br>
                        <p> Pozdrawiam </br>Iguana do Wycinania</p>
                         <p>
                            <img src = ""https://static.wixstatic.com/media/ee3f9d_7870b6387db944f9bda351d13b018dee~mv2.jpg/v1/fill/w_157,h_157,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/Iguana_do_wycinania_Logo.jpg"" >
                         </p>
                     </body>
                </html> "
            ;
        }
    }
}
