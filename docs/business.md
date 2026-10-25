# StayFlow – opis biznesowy

## Czym jest StayFlow

StayFlow to system typu smart hotel. Pozwala gościom obsłużyć większość spraw samodzielnie przez telefon, a personelowi hotelu ogarniać codzienną pracę: sprzątanie, usterki i zamówienia gości.

StayFlow nie jest systemem rezerwacji ani PMS. Zakładam, że hotel ma swój system rezerwacji, a do StayFlow trafiają gotowe pobyty. W projekcie wprowadza je personel w panelu.

## Kto korzysta z systemu

- Gość – melduje się online, sprawdza informacje o pobycie, zamawia usługi, zgłasza usterki.
- Recepcja – wprowadza pobyty, pilnuje przyjazdów, rozwiązuje problemy z odprawą.
- Pokojowa – dostaje zadania sprzątania w aplikacji mobilnej, wypełnia checklisty.
- Technik – dostaje zgłoszenia usterek i oznacza je jako naprawione.
- Manager – przydziela zadania, kontroluje pokoje, przegląda raporty.
- Administrator – zarządza użytkownikami, uprawnieniami, treściami i ustawieniami.

Gość korzysta z przeglądarki na telefonie, bez instalowania aplikacji. Personel ma panel webowy, a pokojowe, technicy i managerowie także aplikację mobilną.

## Procesy

### Odprawa online

1. Gość dostaje link do odprawy, np. mailem przed przyjazdem.
2. Wypełnia dane, sprawdza informacje o pobycie i akceptuje regulamin.
3. System sprawdza dane, ważność linku, datę pobytu i czy odprawa nie została już zrobiona.
4. Jeśli wszystko się zgadza, odprawa kończy się automatycznie, bez recepcji.
5. Gość dostaje dostęp do portalu: usługi, usterki, informacje o pobycie.

Hotelarz nie zatwierdza każdej odprawy. Widzi, kto jest zameldowany, kto jeszcze nie, i ręcznie rozwiązuje wyjątki.

System nie steruje zamkami. Hotel ustawia kod (np. do skrytki z kluczem), a StayFlow pokazuje go gościowi dopiero, gdy:

- odprawa jest zakończona,
- pobyt jest opłacony albo płatność nie jest wymagana,
- pokój jest posprzątany i sprawny,
- jest dzień przyjazdu i minęła godzina zameldowania.

Płatność to tylko status ustawiany przez personel, bez płatności online.

### Zamawianie usług

Gość wybiera usługę z oferty hotelu (np. śniadanie do pokoju, masaż). System sprawdza dostępność, a jeśli usługa wymaga terminu, pilnuje, żeby nie było konfliktu. Zamówienie trafia do personelu, ktoś je realizuje i zamyka. Gość widzi status zamówienia.

### Housekeeping

Po wyjeździe gościa pokój dostaje status "brudny" i powstaje zadanie sprzątania. Manager przydziela je pokojowej. Pokojowa w aplikacji mobilnej wypełnia checklistę, może zgłosić problem i oznacza pracę jako skończoną. Manager może skontrolować pokój i potwierdzić, że jest gotowy.

Pokój ma osobno status czystości, status techniczny i zajętość.

### Usterki

Usterkę może zgłosić gość albo pracownik. Zgłoszenie ma priorytet, jest przypisywane technikowi, który aktualizuje postęp i zamyka sprawę. Do zgłoszenia można dodawać komentarze.

## Czego StayFlow nie robi

- rezerwacji noclegów,
- płatności online,
- sterowania zamkami, kamerami, czujnikami,
- integracji z Booking.com, Airbnb, PMS,
- czatu na żywo,
- księgowości.
