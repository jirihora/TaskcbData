# Task: cbData

# Agregátor objednávek

Navrhnìte webovou službu, která:

- Je napsaná v aktuální verzi .NET.
- Nabízí RESP API endpoint pro pøijetí jedné nebo více objednávek ve formátu:
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
- Objednávky se pro další zpracování agregují - sèítají se poèty kusù dle Id produktu.
- Agregované objednávky se ne èastìji, než jednou za 20 vteøin, odešlou internímu systému - pro naše úèely lze pouze naznaèit a vypisovat JSON do konzole.
- Služba by mìla poèítat s možností velkého množství malých objednávek (stovky za vteøinu) pro relativnì limitovaný poèet Id produktù (stovky celkem).
- Zpùsob persistence dat by mìl být rozšiøitelný a konfigurovatelný - pro naše úèely bude staèit implementovat držení dat v pamìti.
- Kód by mìl obsahovat (alespoò nìjaké) testy.
- Zkuste navrhnout další možná vylepšení a pøímo je implementujte nebo jen naznaète / popište.
- Mìjte kód takový, jako si pøedstavuje v produkèní aplikaci.
- Kód odevzdejte nejlépe formou publikace na GitHub - možno i jako privátní repozitáø.

---

### návrhy na vylepšení:
- Použit nìjaký messaging system (Kafka, RabbitMQ) namísto InMemoryChannels.
- Pøidat autentizaci (napø. pomocí JWT).