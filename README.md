# SPK-the-Game - Projekt-Zespołowy
# Cel projektu
Celem projektu jest stworzenie gry jednoosobowej w środowisku Unity, która łączyłaby w sobie elementy gier karcianych oraz gier rytmicznych.

# Słownik domenowy

**Gracz** - Osoba rozpoczynająca rozrywkę, posiadająca swój licznik Zdrowia, Talię, tzw. Rękę oraz Discard, a także własną Pięciolinię. Gracz przegrywa, gdy jego licznik Zdrowia wskaże 0. 
**Wróg** - Odpowiednik Gracza kontrolowany przez komputer, posiadający swój licznik Zdrowia oraz Talię. Gracz wygrywa, gdy licznik Zdrowia Wroga wskaże 0.
**Karta** - Obiekt posiadający swój typ (np. Atak, Blok, Leczenie, Unik, Dobór) oraz wartość (a także odpowiadający mu sprite). <br>
**Talia** - Zbiór kart, spośród których gra dokonuje Doboru kart; Gracz i Wróg posiadają osobne talie.<br>
**Ręka** - Zbiór maksymalnie 4 kart, z którymi w danej chwili Gracz może wejść w interację, albo wykładając którąś na Pięciolinię, albo odrzucając ją do Discard pile'u.<br>
**Discard** - Zbiór kart, które w ostatnim czasie zostały wywołane na Pięciolinii bądź zostały odrzucone z ręki gracza. Discard pile opróżnia się, kiedy tylko dochodzi do Tasowania. <br>
**Pięciolinia** - Kolejka co najwyżej 5 Kart, przesuwająca karty na niej leżące w prawo. Jeżeli Karta zostaje zagrana z Ręki na Pięciolinię, ta Karta zostaje umieszczona w najbardziej wysuniętym na prawo wolnym miejscu.<br>
Zarówno Gracz, jak i Wróg posiadają osobną Pięciolinię. Pod koniec każdego Taktu najbardziej wysunięta na prawo Karta Gracza zostaje wywołana i porównana z analogiczną Kartą na pięciolinii Wroga.
**Tasowanie** - Przerzucenie kart z Discard do Talii w losowej kolejności w momencie, gdy Talia stanie się pusta.<br>
**Cmentarzysko** - Zbiór Kart zniszczonych, czyli takich, które nie pojawią się więcej w trakcie rozgrywki.
**Takt** - Przedział czasu, po upływie którego gra wywoła najstarszą kartę Gracza i Wroga na Pięciolinii, porówna je ze sobą i na podstawie tego porównania decyduje, jak wpłynąć na statystyki obu stron.<br> 
**Zdrowie** - Liczba całkowita nieujemna reprezentująca, ile Obrażeń może jeszcze przyjąć dana strona, zanim<br>
**Obrażenia** - Wartość, o jaką zmniejsza się Zdrowie oponenta w danym Takcie.<br>
**Atak** - Zadanie Obrażeń stronie przeciwnej.<br>
**Blok** - Zmniejszenie Obrażeń otrzymanych w danym Takcie ze strony przeciwnej. <br>
**Leczenie** - Zwiększenie licznika Zdrowia którejś ze stron o zadaną wartość, nieprzekraczając wartości Zdrowia danej strony z początku rozgrywki.<br>
**Unik** - Szansa na negację wszelkich Obrażeń, jakie dana strona mogłaby otrzymać w danym Takcie.<br>
**Dobór** - dodanie karty z Talii do Ręki.

# Zasady gry 
Celem rozgrywki jest pokonanie wrogów jeden po drugim. W tym celu gracz zagrywa w dowolnym momencie karty z ręki na pięciolinię, o ile na niej są wolne miejsca. Sama rozgrywka utrzymuje się w rytm taktów.
Co X sekund następuje "tik zegara" kończący takt, w którego momencie kolejno wywoływane są najbardziej wysunięte na prawo karty na pięciolinii (jeśli na pięciolinii gracza w chwili wywołania nie ma karty, otrzymuje on poważną karę). W ramach wywołania ta karta gracza oraz analogiczna karta wroga zostają porównane na zasadzie: jeśli wywołana karta z pięciolinii wroga to karta Ataku o wartości 11, a wywołana karta po stronie gracza to też karta Ataku, tym razem o wartości 9, to gracz otrzyma 11 obrażeń, a wróg 9; gdyby jednak gracz wystawił do pięciolinii naprzeciw tej karty kartę Bloku o wartości 8, to w chwili wywołania przez "tik zegara" kart obu stron wróg nie otrzymałby obrażeń, ale sam gracz otrzymałby jedynie 3 punkty obrażeń (mechaniki ataku i bloku mogą się zmienić). Interakcje między innymi kartami muszą działać odpowiednio między sobą. <br>

Gdy karta zostaje wywołana, trafia do discard pile'u, dokąd również może trafić, jeśli graczowi nie spodoba się któraś z kart w ręce i zdecyduje się ją odrzucić. W momencie, gdy talia gracza staje się pusta, dochodzi do przetasowania wszystkich kart z discard pile'u do talii. Pod wpływem niektórych efektów część kart może zostać zniszczona, tym samym permanentnie przeniesiona na cmentarzysko, skąd już nie wrócą więcej do rozgrywki (chyba że gracz użyje karty, która przywoła jakąś zniszczoną kartę z cmentarzyska z powrotem do talii).

Gra kończy się zwycięstwem gracza, gdy pasek zdrowia wroga wskaże 0, natomiast kończy się porażką gracza, jeśli to jego licznik zdrowia zostanie wyzerowany.

# Sterowanie

**1** - Zagraj pierwszą kartę w ręce na pięciolinię <br>
**2** - Zagraj drugą kartę w ręce na pięciolinię <br>
**3** - Zagraj trzecią kartę w ręce na pięciolinię <br>
**4** - Zagraj czwartą kartę w ręce na pięciolinię <br>

**Q** - Odrzuć pierwszą kartę w ręce <br>
**W** - Odrzuć drugą kartę w ręce <br>
**E** - Odrzuć trzecią kartę w ręce <br>
**R** - Odrzuć czwartą kartę w ręce <br>

# Sterowanie awaryjne

**D** - Dobierz manualnie kartę z talii <br>
**Spacebar** - Wywołaj manualnie takt (dobór karty + przesunięcie kolejki na pięciolinii) <br>
**Enter** - Wywołaj manualnie takt bez doboru karty <br>
