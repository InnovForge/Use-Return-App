# Use-Return-App
Cấu trúc thư mục dự án chi tiết:
Use-Return-App/               ← Thư mục solution
│
├─ App_Code/                  ← Các lớp C# thuần (Data Access, Helpers…)
│   └─ DbHelper.cs
│
├─ App_Data/                  ← File .mdf nếu dùng SQL Server Express/LocalDB
│
├─ Content/                   ← CSS & assets tĩnh
│   ├─ site.css
│   └─ bootstrap.min.css
│
├─ Scripts/                   ← JavaScript (jQuery, Bootstrap, custom)
│   ├─ jquery-3.7.1.min.js
│   └─ bootstrap.bundle.min.js
│
├─ Images/                    ← Ảnh minh hoạ
│
├─ MasterPages/               ← Các master page (layout chung)
│   ├─ Site.master
│   └─ Admin.master
│
├─ Pages/                     ← Các trang .aspx con
│   ├─ Default.aspx
│   ├─ Products/
│   │   ├─ List.aspx
│   │   └─ Detail.aspx
│   ├─ Login/
│   │   ├─ List.aspx
│   │   └─ Detail.aspx
│   └─ Something.../
│       ├─ List.aspx
│       └─ Edit.aspx
│
├─ Models/                    ← Lớp POCO (nếu không muốn để App_Code)
│   ├─ Student.cs
│   ├─ Subject.cs
│   └─ Score.cs
│
├─ Global.asax
├─ Web.config
└─ packages.config            ← NuGet (nếu cần)
