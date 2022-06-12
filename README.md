# MyGL

![](/docs/GL-FinancialYearReportExpanded.jpg)

**MyGL** is a personal General Ledger .Net webapp that categorises and provides pivot reports on your bank account transactions.

## Features
  - Categorises transactions
  - Customisable categories and search string rules
  - General Ledger pivot tables per financial or calendar year
  - Customisable pivot tables
  - Import multiple .csv files at once
  - Re-import data without duplication

## Setup

Copy `.env.example` to `.env` and adjust settings as required. Be sure to at least change `SA_PASSWD` to a secure password of your choosing.

Run:
  
`docker-compose up`  

Open http://localhost:9999 (or the port you set in `.env`)