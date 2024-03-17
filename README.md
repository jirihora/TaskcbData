# Task: cbData

# Agreg�tor objedn�vek

Navrhn�te webovou slu�bu, kter�:

- Je napsan� v aktu�ln� verzi .NET.
- Nab�z� RESP API endpoint pro p�ijet� jedn� nebo v�ce objedn�vek ve form�tu:
  ```json
  [
    {
      "productId": "456",
      "quantity": 5
    },
    {
      "productId": "789",
      "quantity": 42
    }
  ]
  ```
- Objedn�vky se pro dal�� zpracov�n� agreguj� - s��taj� se po�ty kus� dle Id produktu.
- Agregovan� objedn�vky se ne �ast�ji, ne� jednou za 20 vte�in, ode�lou intern�mu syst�mu - pro na�e ��ely lze pouze nazna�it a vypisovat JSON do konzole.
- Slu�ba by m�la po��tat s mo�nost� velk�ho mno�stv� mal�ch objedn�vek (stovky za vte�inu) pro relativn� limitovan� po�et Id produkt� (stovky celkem).
- Zp�sob persistence dat by m�l b�t roz�i�iteln� a konfigurovateln� - pro na�e ��ely bude sta�it implementovat dr�en� dat v pam�ti.
- K�d by m�l obsahovat (alespo� n�jak�) testy.
- Zkuste navrhnout dal�� mo�n� vylep�en� a p��mo je implementujte nebo jen nazna�te / popi�te.
- M�jte k�d takov�, jako si p�edstavuje v produk�n� aplikaci.
- K�d odevzdejte nejl�pe formou publikace na GitHub - mo�no i jako priv�tn� repozit��.

---

### n�vrhy na vylep�en�:
- Pou�it n�jak� messaging system (Kafka, RabbitMQ) nam�sto InMemoryChannels.
- P�idat autentizaci (nap�. pomoc� JWT).