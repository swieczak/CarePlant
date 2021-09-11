-- phpMyAdmin SQL Dump
-- version 4.7.9
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Czas generowania: 11 Wrz 2021, 00:15
-- Wersja serwera: 5.7.21
-- Wersja PHP: 5.6.35

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `kwiotki`
--

DELIMITER $$
--
-- Procedury
--
DROP PROCEDURE IF EXISTS `licz`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `licz` (IN `nr_akcji` INT, IN `nr_kwiatka` INT)  MODIFIES SQL DATA
    SQL SECURITY INVOKER
BEGIN
SELECT d.czas_wykonania, z.kiedy_wykonac
INTO @a, @b
FROM (dzienniki_akcji d 
      RIGHT JOIN zadania z ON d.fk_akcji = z.fk_akcji 
      	AND d.fk_zestaw = z.fk_zestaw) 
      WHERE z.fk_akcji = nr_akcji 
      	AND z.fk_zestaw = nr_kwiatka
        ORDER BY d.czas_wykonania DESC LIMIT 1;

SELECT @a,
(CASE WHEN ISNULL(@a) THEN CURRENT_TIMESTAMP()
ELSE @a + INTERVAL @b day
END) AS czas_nastepnego;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `akcje`
--

DROP TABLE IF EXISTS `akcje`;
CREATE TABLE IF NOT EXISTS `akcje` (
  `id_akcji` int(11) NOT NULL AUTO_INCREMENT,
  `opis_akcji` varchar(60) COLLATE utf8_polish_ci NOT NULL,
  PRIMARY KEY (`id_akcji`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `akcje`
--

INSERT INTO `akcje` (`id_akcji`, `opis_akcji`) VALUES
(1, 'Podlanie rośliny'),
(2, 'Nawożenie rośliny'),
(3, 'Usunięcie kwiatów'),
(4, 'Przycięcie liści'),
(5, 'Przesadzenie rośliny'),
(6, 'Zroszenie rośliny'),
(7, 'Zmiana stanowiska');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `dzienniki_akcji`
--

DROP TABLE IF EXISTS `dzienniki_akcji`;
CREATE TABLE IF NOT EXISTS `dzienniki_akcji` (
  `id_dziennika` int(11) NOT NULL AUTO_INCREMENT,
  `czas_wykonania` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fk_akcji` int(11) NOT NULL,
  `fk_zestaw` int(11) NOT NULL,
  PRIMARY KEY (`id_dziennika`),
  KEY `fk_akcji` (`fk_akcji`),
  KEY `fk_zestaw` (`fk_zestaw`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `dzienniki_akcji`
--

INSERT INTO `dzienniki_akcji` (`id_dziennika`, `czas_wykonania`, `fk_akcji`, `fk_zestaw`) VALUES
(10, '2021-07-28 21:29:13', 1, 5),
(11, '2021-08-05 22:18:47', 1, 5),
(15, '2021-07-29 17:11:44', 1, 5),
(16, '2021-07-29 17:39:30', 1, 7),
(17, '2021-07-29 17:39:51', 5, 7),
(18, '2021-07-29 18:18:54', 2, 7),
(19, '2021-07-29 18:19:48', 1, 8),
(20, '2021-07-29 18:35:03', 7, 5),
(21, '2021-09-10 23:43:22', 1, 14);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `gatunki`
--

DROP TABLE IF EXISTS `gatunki`;
CREATE TABLE IF NOT EXISTS `gatunki` (
  `id_gatunki` int(11) NOT NULL AUTO_INCREMENT,
  `fk_rodziny` int(11) NOT NULL,
  `nazwa` char(30) COLLATE utf8_polish_ci NOT NULL,
  `nazwa_lacinska` char(30) COLLATE utf8_polish_ci NOT NULL,
  PRIMARY KEY (`id_gatunki`),
  KEY `fk_gatunki` (`fk_rodziny`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=105 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `gatunki`
--

INSERT INTO `gatunki` (`id_gatunki`, `fk_rodziny`, `nazwa`, `nazwa_lacinska`) VALUES
(1, 1, 'nefrolepis', 'nephropepis'),
(2, 1, 'podrzeń', 'blechnum'),
(3, 1, 'paprotnik', 'cyrtomium'),
(4, 1, 'flebodium', 'phlebodium'),
(5, 1, 'ciemnotka', 'pellaea'),
(6, 1, 'adiantum klinowate', 'adiantum raddianum'),
(7, 2, 'pieprzówka kędzierzawa', 'peperomia caperata'),
(8, 2, 'pieprzówka kluzjolistna', 'peperomia clusiifolia'),
(9, 2, 'pieprzówka magnoliolistna', 'peperomia magnoliifolia'),
(10, 2, 'pieprzówka tępolistna', 'peperomia obtusifolia'),
(11, 2, 'pieprzówka gładka', 'peperomia glabella'),
(12, 2, 'pieprzówka srebrzysta', 'peperomia argyeia'),
(13, 2, 'pieprzówka ferreyrae', 'peperomia ferreyrae'),
(14, 2, 'pieprzówka zwisająca', 'peperomia prostrata'),
(15, 4, 'grubosz drzewiasty', 'crassula arborescens'),
(16, 4, 'grubosz owalny', 'crassula ovata'),
(17, 4, 'grubosz dziurkowany', 'crassula perforata'),
(18, 4, 'grubosz szablasty', 'crassula falcata'),
(19, 4, 'grubosz widełkowaty', 'crassula muscosa'),
(20, 4, 'grubosz Schmidta', 'crassula Schmidtii'),
(21, 4, 'rozchodnik Morgana', 'sedum morganium'),
(22, 4, 'eszeweria', 'echeveria'),
(23, 4, 'eonium drzewiaste', 'antropurpureum'),
(24, 5, 'rosiczka', 'drosera'),
(25, 5, 'dzbanecznik', 'nepenthes'),
(26, 5, 'muchołówka', 'dionaea muscipula'),
(27, 6, 'anturium Andreego', 'anthurium andreanum'),
(28, 6, 'epipremnum złociste', 'epipremnum aureum'),
(29, 6, 'alokazja amazońska', 'alocasia amazonica'),
(30, 6, 'alokazja olbrzymia', 'alocasia macrorrhiza'),
(31, 6, 'zakleśń barwna', 'alocasia veitchii'),
(32, 6, 'filodendron pnący', 'philodendron scandens'),
(33, 6, 'filodendron czerwieniejący', 'philodendron erubescens'),
(34, 6, 'filodendron ciemnozłoty', 'philodendron melonachrysum'),
(35, 6, 'filodendron pierzasty', 'philodendron bipinnatifidum'),
(36, 6, 'skrzydłokwiat kwiecisty', 'spathiphyllum floribundum'),
(37, 6, 'skrzydłokwiat Wallisa', 'spathiphyllum wallisi'),
(38, 6, 'aglaonema zmienna', 'alaonema commutatum'),
(39, 6, 'kaladium dwubarwne', 'caladium bicolor'),
(40, 6, 'monstera dziurawa', 'monstera deliciosa'),
(41, 6, 'scindapsus pstry', 'scindapsus pictus'),
(42, 6, 'zamiokulkas zamiolistny', 'zamioculcas zamiifolia'),
(43, 6, 'zroślicha stopowcowa', 'syngonium podophyllum'),
(44, 6, 'monstera Monkey Mask', 'monstera adansonii'),
(45, 6, 'monstera perforowana', 'monstera obliqua'),
(46, 7, 'juka gwatemalska', 'yucca guatemalensis'),
(47, 7, 'juka aloesowa', 'yucca aloifolia'),
(48, 7, 'juka wyniosła', 'yucca elata'),
(49, 7, 'fatsja japońska', 'fatsia japonica'),
(50, 7, 'aralia Ming', 'polyscicas fruticosa'),
(51, 7, 'aralia Fabian', 'polyscias balfouriana'),
(52, 8, 'figowiec benjamina', 'ficus benjamina'),
(53, 8, 'figowiec sprężysty', 'ficus elastica'),
(54, 8, 'figowiec dębolistny', 'ficus lyrata'),
(55, 8, 'figowiec tępy', 'ficus microcarpa'),
(56, 9, 'kaptur biskupa', 'astropythum myriostigma'),
(57, 9, 'głowa starca', 'cefalosereus'),
(58, 9, 'echinocereus', 'echinocereus pectinatus'),
(59, 9, 'ferokaktus', 'ferocactus'),
(60, 9, 'mamilaria', 'mammillaria'),
(61, 9, 'melokaktus amoneus', 'melocactus amoneus'),
(62, 9, 'melokaktus azureus', 'melocactus azureus'),
(63, 9, 'opuncja', 'opuntia mill'),
(64, 9, 'patyczak', 'rhipsalis'),
(65, 9, 'szlumberga', 'schlumbergera'),
(66, 10, 'agawa amerykańska', 'agave americana'),
(67, 10, 'aloes pstry', 'aloe variegata'),
(68, 10, 'aspidistra wyniosła', 'aspidistra elatior'),
(69, 10, 'dracena obrzeżona', 'dracaena marginata'),
(70, 10, 'dracena wonna', 'dracaena fragrans'),
(71, 10, 'dracena Sandera', 'dracaena sanderiana'),
(72, 10, 'kordylina australijska', 'cordyline australis'),
(73, 10, 'sagowiec odwinięty', 'cycas revoluta'),
(74, 10, 'sansewieria gwinejska', 'sansevieria trifasciata'),
(75, 10, 'sansewieria cylindryczna', 'sansevieria cylindrica'),
(76, 10, 'haworsja wstęgowata', 'haworthia fasciata'),
(77, 10, 'haworsja limifolia', 'haworthia limifolia'),
(78, 11, 'maranta dwubarwna', 'maranta bicolor'),
(79, 11, 'kalatea lancelolistna', 'calathea lancifolia'),
(80, 11, 'kalatea okrągłolistna', 'calathea orbifolia'),
(81, 11, 'kalatea Sanderiana', 'calathea ornata'),
(82, 11, 'kalatea Medallion', 'calathea medallion'),
(83, 12, 'róża ogrodowa', 'rosa'),
(84, 12, 'malina właściwa', 'rubus idaeus'),
(85, 12, 'truskawka', 'fragaria duchesne'),
(86, 13, 'begonia Masona', 'begonia masoniana'),
(87, 13, 'begonia bulwiasta', 'begonia tuberhybrida'),
(88, 13, 'begonia koralowa', 'begonia maculata'),
(89, 13, 'begonia zimowa', 'begonia xhiemalis'),
(90, 3, 'trzykrotka wężykowata', 'tradescantia fluminensis'),
(91, 3, 'smużyna', 'callisia repens'),
(92, 3, 'pasiatka', 'tradescantia'),
(93, 15, 'afelandra czworokątna', 'aphelandra squarrosa'),
(94, 14, 'echmea wstęgowana', 'aechmea fasciata'),
(95, 14, 'ananas', 'ananas mill'),
(96, 15, 'fitonia Verschaffelta', 'fittonia verschaffeltii'),
(98, 15, 'niedośpian ognisty', 'hypoestes phyllostachya'),
(99, 15, 'jakobinia różowawa', 'justicia carnea'),
(100, 16, 'kariota miękka', 'caryota mitis'),
(101, 16, 'karłatka niska', 'chamaerops humilis'),
(102, 16, 'areka', 'chrysalidocarpus lutescens'),
(103, 17, 'cyklamen perski', 'cyclamen persicum'),
(104, 17, 'pierwiosnek kubkowaty', 'primula obconica');

-- --------------------------------------------------------

--
-- Zastąpiona struktura widoku `gatunki_ext`
-- (Zobacz poniżej rzeczywisty widok)
--
DROP VIEW IF EXISTS `gatunki_ext`;
CREATE TABLE IF NOT EXISTS `gatunki_ext` (
`id_gatunki` int(11)
,`nazwa_gatunku` char(30)
,`id_rodziny` int(15)
,`nazwa_rodziny` char(20)
);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `kto_ma_co`
--

DROP TABLE IF EXISTS `kto_ma_co`;
CREATE TABLE IF NOT EXISTS `kto_ma_co` (
  `id_zestaw` int(11) NOT NULL AUTO_INCREMENT,
  `fk_osoby` int(11) NOT NULL,
  `fk_kwiat` int(11) NOT NULL,
  `nazwa` text COLLATE utf8_polish_ci NOT NULL,
  PRIMARY KEY (`id_zestaw`),
  KEY `fk_osoby` (`fk_osoby`) USING BTREE,
  KEY `fk_kwiat` (`fk_kwiat`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `kto_ma_co`
--

INSERT INTO `kto_ma_co` (`id_zestaw`, `fk_osoby`, `fk_kwiat`, `nazwa`) VALUES
(5, 1, 86, 'W kuchni'),
(7, 4, 63, 'W salonie na półce'),
(8, 4, 83, 'Róża'),
(9, 1, 87, 'Drugi'),
(10, 1, 104, 'Jeff2'),
(13, 7, 91, 'Parapet1'),
(14, 4, 49, 'a');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `latwosc`
--

DROP TABLE IF EXISTS `latwosc`;
CREATE TABLE IF NOT EXISTS `latwosc` (
  `id_latwosc` int(11) NOT NULL AUTO_INCREMENT,
  `poziom` enum('początkujący','średniozaawansowany','zaawansowany','') COLLATE utf8_polish_ci NOT NULL,
  PRIMARY KEY (`id_latwosc`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `latwosc`
--

INSERT INTO `latwosc` (`id_latwosc`, `poziom`) VALUES
(1, 'początkujący'),
(2, 'średniozaawansowany'),
(3, 'zaawansowany');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `nawoz`
--

DROP TABLE IF EXISTS `nawoz`;
CREATE TABLE IF NOT EXISTS `nawoz` (
  `id_nawoz` int(11) NOT NULL AUTO_INCREMENT,
  `nazwa` enum('zielone','do paproci','do kaktusów','do kwitnących','brak') COLLATE utf8_polish_ci NOT NULL,
  PRIMARY KEY (`id_nawoz`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `nawoz`
--

INSERT INTO `nawoz` (`id_nawoz`, `nazwa`) VALUES
(1, 'zielone'),
(2, 'do paproci'),
(3, 'do kaktusów'),
(4, 'do kwitnących'),
(5, 'brak');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `osoby`
--

DROP TABLE IF EXISTS `osoby`;
CREATE TABLE IF NOT EXISTS `osoby` (
  `id_osoby` int(11) NOT NULL AUTO_INCREMENT,
  `user` text COLLATE utf8_polish_ci NOT NULL,
  `password` text COLLATE utf8_polish_ci NOT NULL,
  `imie` char(20) COLLATE utf8_polish_ci NOT NULL,
  `nazwisko` char(20) COLLATE utf8_polish_ci NOT NULL,
  PRIMARY KEY (`id_osoby`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `osoby`
--

INSERT INTO `osoby` (`id_osoby`, `user`, `password`, `imie`, `nazwisko`) VALUES
(1, 'aaa', '47bce5c74f589f4867dbd57e9ca9f808', 'Adam', 'Abacki'),
(2, 'bbb', '08f8e0260c64418510cefb2b06eee5cd', 'Bogdan', 'Babacki'),
(3, 'ccc', '9df62e693988eb4e1e1444ece0578579', 'Celina', 'Cabacka'),
(4, 'ddd', '77963b7a931377ad4ab5ad6a9cd718aa', 'Damian', 'Dabacki'),
(5, 'eee', 'd2f2297d6e829cd3493aa7de4416a18f', 'Edward', 'Ebacki'),
(6, 'fff', '343d9040a671c45832ee5381860e2996', 'fff', 'fff'),
(7, '', 'd41d8cd98f00b204e9800998ecf8427e', '', ''),
(8, 'ggg', 'ba248c985ace94863880921d8900c53f', 'ggg', 'ggg');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `ozdoba`
--

DROP TABLE IF EXISTS `ozdoba`;
CREATE TABLE IF NOT EXISTS `ozdoba` (
  `id_ozdoba` int(11) NOT NULL AUTO_INCREMENT,
  `nazwa` enum('liście','kwiaty','liście/kwiaty','') COLLATE utf8_polish_ci NOT NULL,
  PRIMARY KEY (`id_ozdoba`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `ozdoba`
--

INSERT INTO `ozdoba` (`id_ozdoba`, `nazwa`) VALUES
(1, 'kwiaty'),
(2, 'liście'),
(3, 'liście/kwiaty');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `podlewanie`
--

DROP TABLE IF EXISTS `podlewanie`;
CREATE TABLE IF NOT EXISTS `podlewanie` (
  `id_podlewanie` int(11) NOT NULL AUTO_INCREMENT,
  `poziom` enum('mało','średnio','dużo','') COLLATE utf8_polish_ci NOT NULL,
  `interwal` int(11) NOT NULL,
  PRIMARY KEY (`id_podlewanie`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `podlewanie`
--

INSERT INTO `podlewanie` (`id_podlewanie`, `poziom`, `interwal`) VALUES
(1, 'mało', 9),
(2, 'średnio', 4),
(3, 'dużo', 2);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `podloze`
--

DROP TABLE IF EXISTS `podloze`;
CREATE TABLE IF NOT EXISTS `podloze` (
  `id_podloze` int(11) NOT NULL AUTO_INCREMENT,
  `nazwa` enum('zielone','kaktus','glina','piasek') COLLATE utf8_polish_ci NOT NULL,
  PRIMARY KEY (`id_podloze`),
  KEY `nazwa` (`nazwa`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `podloze`
--

INSERT INTO `podloze` (`id_podloze`, `nazwa`) VALUES
(1, 'zielone'),
(2, 'kaktus'),
(3, 'glina'),
(4, 'piasek');

-- --------------------------------------------------------

--
-- Zastąpiona struktura widoku `potrzeby`
-- (Zobacz poniżej rzeczywisty widok)
--
DROP VIEW IF EXISTS `potrzeby`;
CREATE TABLE IF NOT EXISTS `potrzeby` (
`id_rodziny` int(15)
,`nazwa` char(20)
,`latwosc` enum('początkujący','średniozaawansowany','zaawansowany','')
,`ozdoba` enum('liście','kwiaty','liście/kwiaty','')
,`stanowisko` enum('słoneczne','półcień','cień','rozproszone')
,`podlewanie` enum('mało','średnio','dużo','')
,`wilgoc` enum('suche','wilgotne','średnio wilgotne','')
,`podloze` enum('zielone','kaktus','glina','piasek')
,`nawoz` enum('zielone','do paproci','do kaktusów','do kwitnących','brak')
);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `rodziny`
--

DROP TABLE IF EXISTS `rodziny`;
CREATE TABLE IF NOT EXISTS `rodziny` (
  `id_rodziny` int(15) NOT NULL AUTO_INCREMENT,
  `nazwa` char(20) COLLATE utf8_polish_ci NOT NULL,
  `fk_stanowisko` int(11) NOT NULL,
  `fk_podlewanie` int(11) NOT NULL,
  `fk_podloze` int(11) NOT NULL,
  `fk_nawoz` int(11) NOT NULL,
  `fk_latwosc` int(11) NOT NULL,
  `fk_ozdoba` int(11) NOT NULL,
  `fk_wilgoc` int(11) NOT NULL,
  PRIMARY KEY (`id_rodziny`),
  KEY `fk_stanowisko` (`fk_stanowisko`),
  KEY `fk_podlewanie` (`fk_podlewanie`),
  KEY `fk_podloze` (`fk_podloze`),
  KEY `fk_nawoz` (`fk_nawoz`),
  KEY `fk_latwosc` (`fk_latwosc`),
  KEY `fk_ozdoba` (`fk_ozdoba`),
  KEY `fk_wilgoc` (`fk_wilgoc`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `rodziny`
--

INSERT INTO `rodziny` (`id_rodziny`, `nazwa`, `fk_stanowisko`, `fk_podlewanie`, `fk_podloze`, `fk_nawoz`, `fk_latwosc`, `fk_ozdoba`, `fk_wilgoc`) VALUES
(1, 'paprocie', 2, 3, 1, 2, 1, 2, 2),
(2, 'pieprzowate', 4, 2, 1, 1, 1, 2, 3),
(3, 'komelinowe', 4, 3, 1, 1, 1, 3, 2),
(4, 'gruboszowate', 1, 1, 2, 3, 2, 2, 1),
(5, 'rosiczkowate', 4, 3, 1, 5, 2, 1, 3),
(6, 'obrazkowate', 2, 2, 1, 1, 2, 2, 3),
(7, 'araliowate', 2, 3, 1, 1, 1, 2, 2),
(8, 'morwowate', 4, 2, 1, 1, 2, 2, 1),
(9, 'kaktusowate', 1, 1, 2, 3, 1, 3, 1),
(10, 'szparagowate', 4, 1, 2, 1, 1, 2, 1),
(11, 'marantowate', 4, 3, 1, 1, 2, 2, 3),
(12, 'różowate', 1, 3, 3, 4, 3, 1, 3),
(13, 'begoniowate', 4, 1, 1, 1, 3, 1, 3),
(14, 'bromeliowate', 4, 2, 2, 4, 3, 3, 3),
(15, 'akantowate', 1, 3, 1, 1, 2, 1, 3),
(16, 'arekowate', 4, 3, 4, 5, 2, 2, 3),
(17, 'pierwiosnkowate', 4, 3, 1, 4, 1, 1, 2);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `stanowisko`
--

DROP TABLE IF EXISTS `stanowisko`;
CREATE TABLE IF NOT EXISTS `stanowisko` (
  `id_stanowisko` int(11) NOT NULL AUTO_INCREMENT,
  `nazwa` enum('słoneczne','półcień','cień','rozproszone') COLLATE utf8_polish_ci NOT NULL,
  PRIMARY KEY (`id_stanowisko`),
  KEY `nazwa` (`nazwa`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `stanowisko`
--

INSERT INTO `stanowisko` (`id_stanowisko`, `nazwa`) VALUES
(1, 'słoneczne'),
(2, 'półcień'),
(3, 'cień'),
(4, 'rozproszone');

-- --------------------------------------------------------

--
-- Zastąpiona struktura widoku `todosy`
-- (Zobacz poniżej rzeczywisty widok)
--
DROP VIEW IF EXISTS `todosy`;
CREATE TABLE IF NOT EXISTS `todosy` (
`fk_osoby` int(11)
,`id_akcji` int(11)
,`opis_akcji` varchar(60)
,`id_zestaw` int(11)
,`nazwa` text
,`ost_wykonanie` timestamp
,`kiedy_wykonac` int(11)
);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `wilgoc`
--

DROP TABLE IF EXISTS `wilgoc`;
CREATE TABLE IF NOT EXISTS `wilgoc` (
  `id_wilgoc` int(11) NOT NULL AUTO_INCREMENT,
  `poziom` enum('suche','wilgotne','średnio wilgotne','') COLLATE utf8_polish_ci NOT NULL,
  PRIMARY KEY (`id_wilgoc`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `wilgoc`
--

INSERT INTO `wilgoc` (`id_wilgoc`, `poziom`) VALUES
(1, 'suche'),
(2, 'wilgotne'),
(3, 'średnio wilgotne');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `zadania`
--

DROP TABLE IF EXISTS `zadania`;
CREATE TABLE IF NOT EXISTS `zadania` (
  `id_zadania` int(11) NOT NULL AUTO_INCREMENT,
  `kiedy_wykonac` int(11) NOT NULL,
  `fk_akcji` int(11) NOT NULL,
  `fk_zestaw` int(11) NOT NULL,
  PRIMARY KEY (`id_zadania`),
  KEY `fk_akcji` (`fk_akcji`),
  KEY `fk_zestaw` (`fk_zestaw`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Zrzut danych tabeli `zadania`
--

INSERT INTO `zadania` (`id_zadania`, `kiedy_wykonac`, `fk_akcji`, `fk_zestaw`) VALUES
(1, 6, 1, 5),
(2, 9, 1, 7),
(3, 2, 1, 8),
(4, 4, 1, 14);

-- --------------------------------------------------------

--
-- Zastąpiona struktura widoku `zestaw_szczegolowy`
-- (Zobacz poniżej rzeczywisty widok)
--
DROP VIEW IF EXISTS `zestaw_szczegolowy`;
CREATE TABLE IF NOT EXISTS `zestaw_szczegolowy` (
`id_zestaw` int(11)
,`fk_osoby` int(11)
,`nazwa` text
,`gatunek` char(30)
,`id_gatunku` int(11)
,`id_rodziny` int(15)
,`nazwa_rodziny` char(20)
,`fk_stanowisko` int(11)
,`fk_podlewanie` int(11)
,`fk_podloze` int(11)
,`fk_nawoz` int(11)
,`fk_wilgoc` int(11)
,`fk_ozdoba` int(11)
);

-- --------------------------------------------------------

--
-- Struktura widoku `gatunki_ext`
--
DROP TABLE IF EXISTS `gatunki_ext`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `gatunki_ext`  AS  select `g`.`id_gatunki` AS `id_gatunki`,`g`.`nazwa` AS `nazwa_gatunku`,`f`.`id_rodziny` AS `id_rodziny`,`f`.`nazwa` AS `nazwa_rodziny` from (`gatunki` `g` join `rodziny` `f` on((`g`.`fk_rodziny` = `f`.`id_rodziny`))) ;

-- --------------------------------------------------------

--
-- Struktura widoku `potrzeby`
--
DROP TABLE IF EXISTS `potrzeby`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `potrzeby`  AS  (select `r`.`id_rodziny` AS `id_rodziny`,`r`.`nazwa` AS `nazwa`,`l`.`poziom` AS `latwosc`,`o`.`nazwa` AS `ozdoba`,`s`.`nazwa` AS `stanowisko`,`p`.`poziom` AS `podlewanie`,`w`.`poziom` AS `wilgoc`,`p2`.`nazwa` AS `podloze`,`n`.`nazwa` AS `nawoz` from (((((((`rodziny` `r` left join `stanowisko` `s` on((`r`.`fk_stanowisko` = `s`.`id_stanowisko`))) left join `podlewanie` `p` on((`r`.`fk_podlewanie` = `p`.`poziom`))) left join `podloze` `p2` on((`r`.`fk_podloze` = `p2`.`nazwa`))) left join `nawoz` `n` on((`r`.`fk_nawoz` = `n`.`nazwa`))) left join `latwosc` `l` on((`r`.`fk_latwosc` = `l`.`poziom`))) left join `ozdoba` `o` on((`r`.`fk_ozdoba` = `o`.`nazwa`))) left join `wilgoc` `w` on((`r`.`fk_wilgoc` = `w`.`id_wilgoc`)))) ;

-- --------------------------------------------------------

--
-- Struktura widoku `todosy`
--
DROP TABLE IF EXISTS `todosy`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `todosy`  AS  select `zes`.`fk_osoby` AS `fk_osoby`,`akc`.`id_akcji` AS `id_akcji`,`akc`.`opis_akcji` AS `opis_akcji`,`zes`.`id_zestaw` AS `id_zestaw`,`zes`.`nazwa` AS `nazwa`,`tmp`.`ost_wykonanie` AS `ost_wykonanie`,`zad`.`kiedy_wykonac` AS `kiedy_wykonac` from (((`zadania` `zad` join `akcje` `akc` on((`zad`.`fk_akcji` = `akc`.`id_akcji`))) join `kto_ma_co` `zes` on((`zes`.`id_zestaw` = `zad`.`fk_zestaw`))) join (select `dzienniki_akcji`.`fk_zestaw` AS `fk_zestaw`,`dzienniki_akcji`.`fk_akcji` AS `fk_akcji`,max(`dzienniki_akcji`.`czas_wykonania`) AS `ost_wykonanie` from `dzienniki_akcji` group by `dzienniki_akcji`.`fk_zestaw`,`dzienniki_akcji`.`fk_akcji`) `tmp` on(((`tmp`.`fk_akcji` = `zad`.`fk_akcji`) and (`tmp`.`fk_zestaw` = `zad`.`fk_zestaw`)))) ;

-- --------------------------------------------------------

--
-- Struktura widoku `zestaw_szczegolowy`
--
DROP TABLE IF EXISTS `zestaw_szczegolowy`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `zestaw_szczegolowy`  AS  select `z`.`id_zestaw` AS `id_zestaw`,`z`.`fk_osoby` AS `fk_osoby`,`z`.`nazwa` AS `nazwa`,`g`.`nazwa` AS `gatunek`,`g`.`id_gatunki` AS `id_gatunku`,`r`.`id_rodziny` AS `id_rodziny`,`r`.`nazwa` AS `nazwa_rodziny`,`r`.`fk_stanowisko` AS `fk_stanowisko`,`r`.`fk_podlewanie` AS `fk_podlewanie`,`r`.`fk_podloze` AS `fk_podloze`,`r`.`fk_nawoz` AS `fk_nawoz`,`r`.`fk_wilgoc` AS `fk_wilgoc`,`r`.`fk_ozdoba` AS `fk_ozdoba` from ((`kto_ma_co` `z` left join `gatunki` `g` on((`z`.`fk_kwiat` = `g`.`id_gatunki`))) left join `rodziny` `r` on((`g`.`fk_rodziny` = `r`.`id_rodziny`))) ;

--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `dzienniki_akcji`
--
ALTER TABLE `dzienniki_akcji`
  ADD CONSTRAINT `dzienniki_akcji_ibfk_1` FOREIGN KEY (`fk_akcji`) REFERENCES `akcje` (`id_akcji`) ON UPDATE CASCADE,
  ADD CONSTRAINT `dzienniki_akcji_ibfk_2` FOREIGN KEY (`fk_zestaw`) REFERENCES `kto_ma_co` (`id_zestaw`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ograniczenia dla tabeli `gatunki`
--
ALTER TABLE `gatunki`
  ADD CONSTRAINT `gatunki_ibfk_1` FOREIGN KEY (`fk_rodziny`) REFERENCES `rodziny` (`id_rodziny`) ON UPDATE CASCADE;

--
-- Ograniczenia dla tabeli `kto_ma_co`
--
ALTER TABLE `kto_ma_co`
  ADD CONSTRAINT `kto_ma_co_ibfk_1` FOREIGN KEY (`fk_kwiat`) REFERENCES `gatunki` (`id_gatunki`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `kto_ma_co_ibfk_2` FOREIGN KEY (`fk_osoby`) REFERENCES `osoby` (`id_osoby`) ON UPDATE CASCADE;

--
-- Ograniczenia dla tabeli `rodziny`
--
ALTER TABLE `rodziny`
  ADD CONSTRAINT `fk_latwosc` FOREIGN KEY (`fk_latwosc`) REFERENCES `latwosc` (`id_latwosc`) ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_nawoz` FOREIGN KEY (`fk_nawoz`) REFERENCES `nawoz` (`id_nawoz`) ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_ozdoba` FOREIGN KEY (`fk_ozdoba`) REFERENCES `ozdoba` (`id_ozdoba`) ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_podlewanie` FOREIGN KEY (`fk_podlewanie`) REFERENCES `podlewanie` (`id_podlewanie`) ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_podloze` FOREIGN KEY (`fk_podloze`) REFERENCES `podloze` (`id_podloze`),
  ADD CONSTRAINT `fk_stanowisko` FOREIGN KEY (`fk_stanowisko`) REFERENCES `stanowisko` (`id_stanowisko`) ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_wilgoc` FOREIGN KEY (`fk_wilgoc`) REFERENCES `wilgoc` (`id_wilgoc`) ON UPDATE CASCADE;

--
-- Ograniczenia dla tabeli `zadania`
--
ALTER TABLE `zadania`
  ADD CONSTRAINT `zadania_ibfk_1` FOREIGN KEY (`fk_akcji`) REFERENCES `akcje` (`id_akcji`) ON UPDATE CASCADE,
  ADD CONSTRAINT `zadania_ibfk_2` FOREIGN KEY (`fk_zestaw`) REFERENCES `kto_ma_co` (`id_zestaw`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
