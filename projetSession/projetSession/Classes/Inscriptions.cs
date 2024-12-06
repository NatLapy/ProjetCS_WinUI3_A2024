using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession.Classes
{
    internal class Inscriptions
    {
        string noIdentificationAdherent = "";
        int idSceanceInscription = 0;
        int noteAppreciation = 0;

        public Inscriptions(string noIdentificationAdherent, int idSceanceInscription, int noteAppreciation)
        {
            this.noIdentificationAdherent = noIdentificationAdherent;
            this.idSceanceInscription = idSceanceInscription;
            this.noteAppreciation = noteAppreciation;
        }

        public string NoIdentificationAdherent
        {
            get
            {
                return noIdentificationAdherent;
            }

            set
            {
                noIdentificationAdherent = value;


            }
        }

        public int IdSceanceInscription
        {
            get
            {
                return idSceanceInscription;
            }

            set
            {
                idSceanceInscription = value;


            }
        }

        public int NoteAppreciation
        {
            get
            {
                return noteAppreciation;
            }

            set
            {
                noteAppreciation = value;


            }
        }

        public override string ToString()
        {
            return noIdentificationAdherent + " " + idSceanceInscription + " " + noteAppreciation;
        }
    }
}
