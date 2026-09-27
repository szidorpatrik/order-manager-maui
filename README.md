# OrderManagerMaui

A delivery and order tracking mobile application built with **.NET MAUI** using pure **C# Markup (0 XAML)**, **CommunityToolkit.MVVM**, local **SQLite** persistence, and native device capabilities.

---

## Tech Stack

* **Framework:** .NET MAUI (.NET 10)
* **Architecture:** MVVM (`CommunityToolkit.Mvvm`)
* **UI:** 100% C# Code-Behind (no XAML)
* **Persistence:** SQLite (`sqlite-net-pcl`)
* **Utilities:** `CommunityToolkit.Maui` (Toasts, Behaviors)
* **Target Platforms:** Android

---

## Key Features

### 1. Data Layer & Persistence

* **Entity Model (`Order`)**: Tracks customer name, delivery address, order total, delivery status (`IsDelivered`), timestamps, and nullable GPS coordinates (`Latitude`, `Longitude`).
* **`DatabaseService`**: Manages asynchronous SQLite CRUD operations with schema bootstrapping during startup (`App.CreateWindow`) to avoid startup race conditions.

### 2. Pure C# UI Architecture & Design Tokens

* **Design Tokens (`AppColor`)**: Centralized semantic color palette (`Primary`, `Secondary`, `Success`, `Warning`, `Danger`, `Surface`, etc.) accessed via `.ToColor()` extension methods.
* **Component-Based UI**:
    * `FabButton`: Standardized 56x56 circular action button with configurable SVG icons, background colors, and drop shadows.
    * `OrderListItem`: List item card featuring dynamic status indicators, tap visual states (`VisualStateManager`), and swipe/button actions.
    * `OrderDetailsCard`: Multi-bound details card displaying formatted currency (`Ft`), delivery metadata, and geo-coordinates formatted as `DD.DDDDD° N, DD.DDDDD° E`.

### 3. Screen Workflows

* **Orders List (`OrdersPage`)**: `CollectionView` with transparent footer spacing for unobstructed scrolling above floating action buttons. Auto-refreshes data on appearance (`OnAppearing`).
* **Order Creation & Edit (`OrderCreatePage`)**: Dual-mode input form with client-side field validation and shell parameter hydration (`[QueryProperty]`).
* **Order Details (`DetailsPage`)**: Detailed order overview with stacked action buttons (Maps, Delivery Confirmation, Edit) and automatic data synchronization upon return.

### 4. Native Hardware & OS Integrations

* **GPS Capture & Confirmation**: Prompts confirmation prior to delivery completion, checks/requests runtime permissions (`Permissions.LocationWhenInUse`), and captures device GPS coordinates via `Geolocation`. Button interactions are locked via `IsBusy` during async retrieval.
* **Native Maps Launch**: Launches the default OS map application (Google Maps / Apple Maps / Waze etc.) pinned to recorded coordinates without initiating turn-by-turn navigation (`NavigationMode.None`).
* **System Clipboard**: Formats and exports order summaries to the system clipboard (`Clipboard.Default`).
* **Native Share Sheet**: Invokes the OS share dialog (`Share.Default`) with structured order payloads.

---

## Project Structure

```text
OrderManagerMaui/
├── Components/         # Reusable UI controls (FabButton, OrderDetailsCard, OrderListItem)
├── Models/             # SQLite entity models (Order)
├── Resources/
│   ├── Images/         # Vector SVGs (edit, share, map, check, etc.)
│   └── Splash/         # Launch screens
├── Services/           # SQLite database services
├── Theme/              # AppColor design tokens and color extensions
├── ViewModels/         # MVVM ViewModels with CommunityToolkit
└── Views/              # Pure C# ContentPages (OrdersPage, DetailsPage, OrderCreatePage)
```

---

## Prerequisites & Setup

### 1. **.NET MAUI Workload**

```bash
dotnet workload install maui-android
```

### 2. **Android Permissions**

Ensure `Platforms/Android/AndroidManifest.xml` includes location declarations:

```xml
<uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
<uses-feature android:name="android.hardware.location" android:required="false" />
<uses-feature android:name="android.hardware.location.gps" android:required="false" />
```

### 3. **Build & Run (Android Emulator/Device)**

```bash
dotnet build -t:Run -f net10.0-android
```
