using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace delegateDemo
{
    //1. wat is een delegate?
    //Een delegate is een managed pointer naar een functie.
    delegate void toonLijstDelegate(List<int> mijnLijst,Form1 frm);

    public partial class Form1 : Form
    {
        //2. Waarvoor worden onderstaande lijsten gebruikt?

        readonly List<int> randomGetallenLijst = new List<int>();
        //Om willekeurige getallen bij te houden.
        public List<Form1> ChildForms = new List<Form1>();
        //Bijhouden van de childforms bij de parentform.

        //3. Leg uit wat een static variabele is? 

        //Static variabelen worden gebruikt om in alle instantie de gedeclareerde variabele aan te passen.
        static int formNummer = 1;
        const int MAX_RANDOMGETALLEN = 10;
        const int MAX_RANDOMWAARDE = 100;

        public Form1()
        {
            InitializeComponent();

            //4. De static int formNummer wordt gebruikt om elke form een volgnummer te geven.
            //Deze staat in de klasse gedefinieerd als 'static int formNummer = 1;'.
            //Hoe komt het dat ondanks die in die declaratie '... = 1' staat elke form toch een 
            //oplopend nummer krijgt? Leg dit grondig uit.

            //In de code hieronder wordt het nummer opgeslaan in de naam en daarna wordt de waarde met 1 opgeteld.
            //Het is omdat het een static variabele zal de wijziging ook in elke andere instantie plaatsvinden.
            this.Name = "Form"+formNummer.ToString();
            Form1.formNummer++;

            btnVulLijstMetRandomGetallen.Text = "Vul List<int> met " + MAX_RANDOMGETALLEN.ToString() + " randomgetallen";
            
            //5. Wat is het effect van het onderstaande statement op de Form?
            
            //De naam die getoont wordt, wordt gelijkgesteld aan de nieuwe naam.
            this.Text = this.Name;
        }

        //6. Wanneer je op de button 'btnMaakNieuweChildForm' klikt dan wordt er een
        //nieuwe childform aangemaakt. Bestudeer grondig de onderstaande method en 
        //omschrijf - in woorden of met een diagram - de stappen die worden
        //uitgevoerd bij het aanmaken van een childform.
        //Leg uit wat de functie is van 'mijnAndereForm', 'OuderForm' en 'ChildForms'. 
        //Geef aan of het over objecten of attributen gaat. Leg uit van welke klasse ze
        //zijn of bij welk object ze horen en omschrijf waarvoor ze gebruikt worden.
        
        private void BtnMaakNieuweChildForm_Click(object sender, EventArgs e)
        {
            Form1 mijnAndereForm = new Form1(); //Aanmaken van nieuwe form instance.
            mijnAndereForm.OuderForm = this; //Nieuwe form krijgt reference naar de parent form.
            ChildForms.Add(mijnAndereForm); //Nieuwe form wordt aan de childlijst toegevoegd.
            mijnAndereForm.Show(); //Nieuwe form wordt zichtbaar.
        }

        //7. Onderstaande method genereert een aantal randomgetallen. Hoeveel getallen 
        //worden er gegenereerd? Wat is het grootste randomgetal dat kan worden 
        //gegenereerd?

        //Er worden random getallen aangemaakt die in een lijst gestopt worden. Het maximum van de random getallen is MAX_RANDOMWAARDE en zal er niet boven gaan.
        private void BtnVulLijstMetRandomGetallen_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            randomGetallenLijst.Clear();

            for (int i=0; i<MAX_RANDOMGETALLEN; i++)
            {
                randomGetallenLijst.Add(random.Next(0, MAX_RANDOMWAARDE));
            }

            foreach (int i in randomGetallenLijst)
            {
                tbLogText.Text += i + "  ";
            }
            tbLogText.Text += Environment.NewLine;
        }

        //8. Waarvoor wordt het property OuderForm gebruikt?

        //Om de variabelen van de parentform te kunnen gebruiken/weergeven.
        private Form1 ouderForm;

        public Form1 OuderForm
        {
            get { return ouderForm; }
            set { ouderForm = value; }
        }

        //9. Je kan de naam van een parent van een childform tonen. Leg uit hoe de onderstaande 
        //method dit realiseert.
        private void BtnToonParentNaam_Click(object sender, EventArgs e)
        {
            if (ouderForm != null)//Controle of er wel een ouderfrom bestaat en dus niet het origineel was.
            {
                MessageBox.Show("De parent van dit child is "+ ouderForm.Name, "aangemaakt vanaf...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //De naam wordt uit ouderform.name gehaald.
            }
            else
            {
                MessageBox.Show("Dit is een bronform", "aangemaakt vanaf...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Error als het het origneel was.
            }
        }

        //10. Je kan de namen van alle childs van een parent tonen. Leg uit hoe de onderstaande 
        //method dit realiseert.
        private void BtnToonChildForms_Click(object sender, EventArgs e)
        {
            if (ChildForms.Count>0) //Alle childs zitten in de childformlijst deze mag niet nul zijn bv als er nog geen zijn aangemaakt.
            {
                foreach (Form1 f in ChildForms) //Met for each wordt er door de lijst gegaan om zo alle namen te tonen.
                {
                    tbLogText.Text +="Childform : " + f.Name + Environment.NewLine;
                }
            }
            else
            {
                tbLogText.Text += "Deze parent heeft geen childforms"+Environment.NewLine;
                //Error als er geen childs zijn aangemaakt bij de parent form in kwestie.
            }
        }

        //11. Onderstaande method toont de random getallen van een child op zijn parent.
        //Dit lijkt een éénvoudige routine, maar schijn bedriegt. Niet elke form heeft immers een parent. 
        //Hoe wordt er bijgehouden of het form een parentform heeft? 
        //
        //Bovendien kan een childform niet zomaar op een parentform gaan
        //schrijven. Er is nog zoiets als thread-safety...Om op een veilige manier data van 
        //een childform naar een parentform te schrijven wordt er gebruik gemaakt van delegates.
        //We hebben dit mechanisme al in de les gezien. Omschrijf voor deze toepassing hoe het
        //delegate-mechanisme zorgt dat data veilig van een childform naar een parentform wordt geschreven
        private void BtnToonGetallenOpParent_Click(object sender, EventArgs e)
        {
            if  (ouderForm!=null) //Checken of het niet de originele form is.
            {
                ToonRandomGetallenOpForm(randomGetallenLijst,ouderForm); //Functie om getallen op de parent te tonen wordt opgeroepen.
            }
            else
            {
                MessageBox.Show("Dit is een bronform. Het heeft geen ouderform..."); //Error als het het origineel is.
            }
        }

        private void ToonRandomGetallenOpForm(List<int> randomGetallen, Form1 frm)
        {           
            if (frm.tbLogText.InvokeRequired) //Thread controle (Wordt het van de juiste geopent).
            {
                var d = new toonLijstDelegate(ToonRandomGetallenOpForm); //Nieuwe delegate wordt gedeclareerd.
                frm.tbLogText.Invoke(d, new object[] { randomGetallenLijst, frm }); //De functie wordt opgeroepen op de thread van de parentform.
            }
            else
            {
                //De foreach werkt wanneer de parentform thread de method uitvoert. En zo de nummers laat weergeven.
                foreach (int i in randomGetallen)
                {
                    frm.tbLogText.Text += i+"  ";
                }
                frm.tbLogText.Text += Environment.NewLine;
            }
        }

        //12. Onderstaande method toont de random getallen van een parent op al zijn children.
        //Dit lijkt weerom een éénvoudige routine, maar schijn bedriegt. 
        //Ook hier moet er rekening gehouden worden met thread-safety...
        //Om op een veilige manier data van een parentform naar een childform te schrijven wordt er 
        //gebruik gemaakt van delegates. We hebben dit mechanisme al in de les gezien.
        //Omschrijf voor deze toepassing hoe het delegate mechanisme zorgt dat data veilig 
        //van de parentform naar de childforms wordt geschreven.
        private void BtnToonGetallenOpChild_Click(object sender, EventArgs e)
        {
                foreach(Form1 f in ChildForms)
                {
                    ToonRandomGetallenOpForm(randomGetallenLijst, f); //Met de foreach wordt er door de lijst childforms gegaan en zo wordt de functie opgeroepen voor elke child.
                }
        }

        //13. Onderstaande method sluit een form. Dat is iets wat met zorg moet gebeuren. Een form kan 
        //immers een parentform en childforms hebben. Referenties daarnaar wil je niet zomaar
        //levend laten...
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Onderstaande lus wordt uitgevoerd wanneer je een willekeurige form sluit.
            //Wat wordt er in deze lus gedaan? 
            //Om dit te testen start je de code op. Form1 opent. Je maakt een Kinder-form Form2 aan. 
            //Vervolgens maak je van Form2 een kinder-form Form3 aan. Sluit dan (met 'x') Form1. 
            //Wat stel je vast?
            
            //Eerst worden alle childs gesloten voor de aangeklikte wordt gesloten.

            //Voer vervolgens onderstaande test uit: maak eerst forms aan volgens onderstaand schema.
            //De -> geeft de parent-child relatie aan. Sluit vervolgens form2. Wat stel je vast?
            //form1 -> form2
            //form1 -> form3
            //form2 -> form4
            //form2 -> form5
            //form4 -> form6
            //form4 -> form7

            //Dat alle onderliggende forms die gebouwd zijn op form2 ook gesloten worden (eerst) en met dezelfde regels zoals de vraag hierboven.

            for (var i = 0; i < ChildForms.Count; i++)
            {
                MessageBox.Show(ChildForms[i].Name.ToString() + " wordt gesloten ");
                ChildForms[i].OuderForm = null;
                ChildForms[i].Close();
                //uncomment het onderstaande if-statement eens, run de code en maak van een parentform 
                //een viertal childforms aan. Wat gebeurt er wanneer je de parent verwijderd? 

                //De ifstatement errort => System.ArgumentNullException: 'Waarde kan niet null zijn.'

                //Hoe kan je ervoor zorgen dat jouw toepassing toch gecontroleerd kan worden gesloten?

                //Al de forms verwijderen en hun referenties  verwijderen.
                if (i == 2) throw (new ArgumentNullException());
            }

            ChildForms.Clear();

            //Waarom is bij het sluiten van een Form het onderstaande if-statement
            //noodzakelijk? 

            //Om forms ook uit de child lijst te halen van hun parent form.

            //Om dit te testen start je de code op. Form1 opent. Je maakt een Kinder-form Form2 aan. 
            //Vervolgens maak je van Form2 een kinder-form Form3 aan. Sluit dan (met 'x') Form2. 
            //Wat stel je vast?

            //De messagebox met "form2 wordt verwijderd uit lijst ChildForms van form1" toont zich.

            if (OuderForm != null)
            {
                var geslotenForm = sender as Form1;
                MessageBox.Show(geslotenForm.Name.ToString() + " wordt verwijderd uit lijst ChildForms van " + OuderForm.Name.ToString());
                OuderForm.ChildForms.Remove(geslotenForm);
            }
        }

        //14. een makkelijke...wat doet de onderstaande method?
        private void BtnClearLogText_Click(object sender, EventArgs e)
        {
            tbLogText.Clear(); 
            //De logtext wordt gecleared/verwijderd.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
