using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class Inscriptions 
    {
        String noIdentificationAdherent = "";
        int idSceanceInscription = 0;
        int noteAppreciation = 0;

        public Inscriptions(string noIdentificationAdherent, int idSceanceInscription, int noteAppreciation)
        {
            this.noIdentificationAdherent = noIdentificationAdherent;
            this.idSceanceInscription = idSceanceInscription;
            this.noteAppreciation = noteAppreciation;
        }

        public String NoIdentificationAdherent
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
