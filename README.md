# DynamicChartApp

Öncelikle veritabanını, tabloyu ve test verilerini oluşturuyoruz.

1.Veritabanı Oluşturma

CREATE DATABASE DynamicChartDB;
GO

2.Dataset Tablosunu Oluşturma

USE DynamicChartDB;
CREATE TABLE Dataset (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Key] NVARCHAR(100) NOT NULL,
    [Value] NVARCHAR(100) NOT NULL
);
GO

3.Test Verileri Ekleme

INSERT INTO Dataset ([Key], [Value])
VALUES 
('Monday', '150'),
('Tuesday ', '100'),
('Wednesday ', '75'),
('Thursday ', '200'),
('Friday ', '125'),
('Saturday', '50'),
('Sunday', '100');
GO

Son olarak ilgili veritabanı ve tablo görünümü şu şekilde olmalı;

![image](https://github.com/user-attachments/assets/1096e935-2627-46f6-b8bc-cc417b5ff55e)

Projeyi çalıştırdıktan sonra;

Gerekli bilgileri girip DB bağlantısını sağlıyoruz;
![1](https://github.com/user-attachments/assets/426dc594-3123-4360-9b64-91d038e381f4)

İlgili tablo içerisinden grafiğe dökmek istediğimiz verileri ve grafik türünü seçiyoruz;
![2](https://github.com/user-attachments/assets/0e502ee3-53b5-4169-b917-5868365a8bda)

Seçilen bilgilere göre grafik oluşuyor;
![3](https://github.com/user-attachments/assets/08657d1e-800f-4c47-8d61-55bba81ced2b)

