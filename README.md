# 🔐 Password → QR Generator

A simple web application that converts passwords into QR codes. Built with ASP.NET Core and vanilla HTML/CSS/JS.

## What it does

You enter one or more passwords, click "Generate QR", and the app instantly creates a QR code for each one. You can also add a label like "Gmail" or "Github" to keep track of which QR belongs to which account. Every QR code can be downloaded as a PNG file.

Passwords are **never stored** on the server. Once you close the page, everything is gone.

## Tech Stack

- **Backend:** ASP.NET Core (.NET 8) — Minimal API
- **QR Generation:** QRCoder
- **Frontend:** HTML, CSS, Bootstrap 5, vanilla JavaScript

## Project Structure

```
PasswordQrGenerator/
├── Program.cs          # API endpoint + QR logic
├── wwwroot/
│   └── index.html      # Frontend
└── PasswordQrGenerator.csproj
```

## Getting Started

**1. Clone or create the project:**
```bash
dotnet new web -n PasswordQrGenerator
cd PasswordQrGenerator
```

**2. Install QRCoder:**
```bash
dotnet add package QRCoder
```

**3. Add the files:**
- Replace `Program.cs` with the backend code
- Create a `wwwroot` folder
- Add `index.html` inside `wwwroot`

**4. Run:**
```bash
dotnet run
```

Open `http://localhost:5000` in your browser.

## How it works

The frontend sends a POST request to `/generate` with a list of label + password pairs. The backend generates a QR code for each password using the QRCoder library, converts it to a Base64 PNG, and sends it back. The frontend then renders each QR as an image card with a download button.

```
Browser → POST /generate → ASP.NET Core → QRCoder → Base64 PNG → Browser
```

## API

**POST** `/generate`

Request body:
```json
[
  { "label": "Gmail", "password": "mypassword123" },
  { "label": "Github", "password": "anotherpassword" }
]
```

Response:
```json
[
  { "label": "Gmail", "base64Image": "iVBORw0KGgo..." },
  { "label": "Github", "base64Image": "iVBORw0KGgo..." }
]
```

## Notes

- Passwords are processed in memory only — nothing is logged or saved
- You can generate multiple QR codes at once
- QR codes can be downloaded individually as `.png` files
