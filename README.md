# Smartsheet-Exporter
An exporter for generating github compatible files for smartsheet sheets.

## Current Design

At the moment, this takes:
1. Your smartsheet API key
2. Your smartsheet sheet id
3. The save location of the json output

All 3 apppear as inputs when you run the exe, so no arguments are able to be directly input yet.

## Future Plans
Long term, I am going to be revising how it is designed and used, to ensure scalability and simple UI.

I _may_ still keep the current input for ease of testing.

The CLI args will most likely be:
1. `--help` (`-h`): displays how to use and the available commands
2. `--setup` (`-i`): initial setup, setting your api key, and any other future persistant settings
3. `--sheetid`(`-id`): id of sheet to export
4. `--apikey` (`-api`): for manually specifying your api key. Enables more secure use, as this won't be stored anywhere.
5. `--savedir` (`-sd`): directory to save the exported sheet to

Further to this, I want to add an option to enable the output layout to be specified.

Users will be able to set which details they want to pull in, and how the info is laid out. 

## Output

### Sample output

```
{
  "id": 12345678,
  "name": "Some Sheet",
  "columns": [
    {
      "id": 101112,
      "name": "SomeColumn,
      "description": "This is some column",
      "formula": "",
      "hidden": false,
      "options": [],
      "primary": true,
      "symbol": null,
      "type": "TEXT_NUMBER",
      "validation": false
    },
    {
      "id": 232425,
      "name": "SomeOtherColumn",
      "description": "Remove the last three letters of some column",
      "formula": "=LEFT(SomeColumn@row, LEN(SomeColumn@row) - 3)",
      "hidden": true,
      "options": [],
      "primary": false,
      "symbol": null,
      "type": "TEXT_NUMBER",
      "validation": false
    },
    ...
  ],
  "samplerow": {
    "0": {
      "DisplayValue": "A cool string val",
      "Formula": null
    },
    "1": {
      "DisplayValue": "A cool string ",
      "Formula": "=LEFT(SomeColumn@row, LEN(SomeColumn@row) - 3)"
    },
    ...
  }
}
```

### Sheet Output
#### id (long)
The id of the sheet

#### name (string)
the name of the sheet

### Column Output
#### id (long)
The id of the column

#### name (string)
The name of the column

#### description (string, nullable)
The description of the column, if it exists

#### formula (string, nullable)
The column formula, if it exists

#### hidden (bool)
Whether this column is visible

#### options (string array, )
The available dropdown values

#### primary (bool)
True if this is the primary column, else false

#### symbol (string, nullable)
Any of the values specified at the official [Smartsheet SDK](https://developers.smartsheet.com/api/smartsheet/openapi/columns#:~:text=different%20column%20types.-,Symbol%20Columns)

Values as at time of writing:
##### CHECKBOX columns

| Value | Example |
| ----- | ------- |
| FLAG  | <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_flagc.1e48f5070b09c52c08873fea300fb30b7d2fa5e1f3517c042067307272c847e9.a83b240f.png" /> |
| STAR  | <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_starc.dcededf26eac79e20845e81d36b0301e60890ca860cd33c374fa98b1f85d5218.a83b240f.png" /> |

#### PICKLIST columns

| Value            | Example |
| ---------------- | ------- |
| ARROWS_3_WAY     |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_arrows3.e2f6ca393bfa42dfe6a6b1a9d015c5c6e8c2304653316aef9df88c47823429a1.a83b240f.png"/> |
| ARROWS_4_WAY     |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_arrows4.f2b0ba478f66b81173e52e97f905131e6302c427a84a54c6560e6f32c4ee4288.a83b240f.png"/> |
| ARROWS_5_WAY     |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_arrows5.3feff262496cdab2d2de21bcfa585de2c4617c5925d00a4ed6cd956c03dbae50.a83b240f.png"/> |
| DECISION_SHAPES  |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_decisionshapes.6d6a3714a0d065492e86df825ec5f91447876aa348247847ce64277672951c2d.a83b240f.png"/> |
| DECISION_SYMBOLS |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_decisionsymbols.e50005dfc73490c40d1a1500b645f3e38fbac770bf195effe99ff43ee74ff822.a83b240f.png"/> |
| DIRECTIONS_3_WAY |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_directions3.388942d64e79db89facc88499b0f977f125f97f85b45f55e9ba7d023626be927.a83b240f.png"/> |
| DIRECTIONS_4_WAY |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_directions4.e9a8e566f4d248b5c49554671e944d6877bd90f301bafcc443b142dd81afa5d9.a83b240f.png"/> |
| EFFORT           |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_effort6three.c0f88d6516271587f1cfb15f747ffd3bc11ed219be7a9af44ca1a9c2146b5c07.a83b240f.png"/> |
| HARVEY_BALLS     |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_harvey5.1213f97d0400b1e0cb957e7584e8feb6b20c8a0e7aa2b902ba206a11b5c8ef32.a83b240f.png"/> |
| HEARTS           |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_heart6three.fa242f946079dd64446e5098b6707872ae1ae3ba3d20ce9fd258b17d44b8e6dd.a83b240f.png"/> |
| MONEY            |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_money6three.fcf24be88730d698225a8098456fcb36dd51de8ea652c98fff57a0a93398f097.a83b240f.png"/> |
| PAIN             |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_pain6.12639cebc225d87fed6fa0dd97de4249a8cf6c61370a28f8b80931791ba32565.a83b240f.png"/> |
| PRIORITY         |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_priority.e1a22386c8405ef85c27512704b5e5d97a8e68472f420e3aa94b4a9a7f628bdc.a83b240f.png"/> |
| PRIORITY_HML     |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_priorityhml.bcb4f13061cbac29317b64c1df662bcbde50b535d03468380ed438d64a0567f6.a83b240f.png"/> |
| PROGRESS         |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_progress5half.9391fac9cd50569e92612b2571e3f9c8f81875ceccfbee88791a7966636955d9.a83b240f.png"/> |
| RYG              |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_ryg.0552eda0d0d6c822074c5d3d5282858508a722cbb624c3e07409e0e49699609f.a83b240f.png"/> |
| RYGB             |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_rygb.189a1f03af95ecfa922fa38d3a64d81c4e863c0d8db5048e5ec247540edf6ea6.a83b240f.png"/> |
| RYGG             |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_rygg.97183f39854d2d53c64c8d5e4469193a7d881d2b23bef57b650ce352b5c1460a.a83b240f.png"/> |
| SIGNAL           |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_signal5.dcf9203d5afd86338425cb43314a6e44e19084abae3992f6baed463d0d4369fe.a83b240f.png"/> |
| SKI              |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_difficulty4.48141dffc2e2d3a537e4639e6681d420a05cdc1184f52ba685f4c5d89aecba25.a83b240f.png"/> |
| STAR_RATING      |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_star6three.7c58aa5136b563642f7868ebee5b09f02c1d638fdd2b89a9cc2aca6e817ce3da.a83b240f.png"/> |
| VCR              |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_decisionvcr.9e73e85d700c8073d167566096e6cb4027d33695964e73afa024bfba71783635.a83b240f.png"/> |
| WEATHER          |  <img width="150" height="16" alt="image" src="https://developers.smartsheet.com/assets/img_pl_weather5.03245852e80b84251fe49d0417602fcdc1ea030910267b0b2ba4866d0a703c65.a83b240f.png"/> |

#### type (string)
Any of the column types specified at the official [Smartsheet SDK](https://developers.smartsheet.com/api/smartsheet/openapi/columns#:~:text=sheet%20or%20report.-,Column%20Types)

Values as at the time of writing:

| Column Type   | Column.type Value  | Notes |
| ------------- | ------------------ | ----- |
| Checkbox      | CHECKBOX           | Checkbox, star, and flag types |
| Contact List  | CONTACT_LIST       | List containing contacts or roles for a project.<br>Note: You can use the contactOptions property to specify a pre-defined list of values for the column, which can also become lanes in card view. |
| Contact List  | MULTI_CONTACT_LIST | List where single cells can contain more than one contact.<br>Only visible when using a query parameter of level and the value appropriate to the dashboard, report, or sheet that you are querying.<br>To see email addresses behind the display names, combine an include=objectValue query parameter with a level query parameter. |
| Date          | DATE               |  |
| Date/Time     | ABSTRACT_DATETIME  | Represents a project sheet's start and end dates.<br>**Only for dependency-enabled project sheets.**<br>The API does not support setting a column to this type. (This can only be done through the Smartsheet Web app when configuring a project sheet.)<br>Additionally, the API does not support updating data in the "End Date" column under any circumstance, and does not support updating data in the "Start Date" column if "Predecessor" is set for that row. |
| Date/Time     | DATETIME           | Used only by the following system-generated columns:<br>- Created (Date) (Column.systemColumnType = CREATED_DATE)<br>- Modified (Date) (Column.systemColumnType = MODIFIED_DATE) |
| Dropdown List | PICKLIST           | Custom, RYG, Harvey ball, priority types, etc. |
| Dropdown List | MULTI_PICKLIST     | List where single cells can contain more than one dropdown item.<br>Only visible when using a query parameter of level and the value appropriate to the dashboard, report, or sheet that you are querying.<br>To see multi-picklist values behind the display names, combine an include=objectValue query parameter with a level query parameter. |
| Duration      | DURATION           | Only for dependency-enabled project sheets.<br>The API does not support setting a column to this type.<br>(This can only be done through the Smartsheet Web app when configuring a project sheet.) |
| Predecessor   | PREDECESSOR        | Defines what must happen first in a project flow.<br>For more information, see the Predecessor object.<br>Only for dependency-enabled project sheets |
| Text/Number   | TEXT_NUMBER        |  |

#### validation (bool)
True if this column must be exactly the type specified, else false
> This is specifically for:
> - Dropdowns (restrict to list values only)
> - Dates (restrict to dates only)
> - Contact Lists (restrict to list values only)
> - Checkboxes (restrict to checkbox use only)
> - Symbols (restrict to symbol values only)
