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
| FLAG  |         |
| STAR  |         |

#### PICKLIST columns

| Value            | Example |
| ---------------- | ------- |
| ARROWS_3_WAY     |         |
| ARROWS_4_WAY     |         |
| ARROWS_5_WAY     |         |
| DECISION_SHAPES  |         |
| DECISION_SYMBOLS |         |
| DIRECTIONS_3_WAY |         |
| DIRECTIONS_4_WAY |         |
| EFFORT           |         |
| HARVEY_BALLS     |         |
| HEARTS           |         |
| MONEY            |         |
| PAIN             |         |
| PRIORITY         |         |
| PRIORITY_HML     |         |
| PROGRESS         |         |
| RYG              |         |
| RYGB             |         |
| RYGG             |         |
| SIGNAL           |         |
| SKI              |         |
| STAR_RATING      |         |
| VCR              |         |
| WEATHER          |         |


#### type (string)
Any of the column types specified at the official [Smartsheet SDK](https://developers.smartsheet.com/api/smartsheet/openapi/columns#:~:text=sheet%20or%20report.-,Column%20Types)

Values as at the time of writing:

| Column Type   | Column.type Value  | Notes |
| ------------- | ------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Checkbox      | CHECKBOX           | Checkbox, star, and flag types                                                                                                                                                                                                                                                                                                                                                                                                                               |
| Contact List  | CONTACT_LIST       | List containing contacts or roles for a project. Note: You can use the contactOptions property to specify a pre-defined list of values for the column, which can also become lanes in card view.                                                                                                                                                                                                                                                             |
| Contact List  | MULTI_CONTACT_LIST | List where single cells can contain more than one contact. Only visible when using a query parameter of level and the value appropriate to the dashboard, report, or sheet that you are querying. To see email addresses behind the display names, combine an include=objectValue query parameter with a level query parameter.                                                                                                                              |
| Date          | DATE               |                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| Date/Time     | ABSTRACT_DATETIME  | Represents a project sheet's start and end dates. **Only for dependency-enabled project sheets.** The API does not support setting a column to this type. (This can only be done through the Smartsheet Web app when configuring a project sheet.) Additionally, the API does not support updating data in the "End Date" column under any circumstance, and does not support updating data in the "Start Date" column if "Predecessor" is set for that row. |
| Date/Time     | DATETIME           | Used only by the following system-generated columns: \n- Created (Date) (Column.systemColumnType = CREATED_DATE) \n- Modified (Date) (Column.systemColumnType = MODIFIED_DATE)                                                                                                                                                                                                                                                                               |
| Dropdown List | PICKLIST           | Custom, RYG, Harvey ball, priority types, etc.                                                                                                                                                                                                                                                                                                                                                                                                               |
| Dropdown List | MULTI_PICKLIST     | List where single cells can contain more than one dropdown item. Only visible when using a query parameter of level and the value appropriate to the dashboard, report, or sheet that you are querying. To see multi-picklist values behind the display names, combine an include=objectValue query parameter with a level query parameter.                                                                                                                  |
| Duration      | DURATION           | Only for dependency-enabled project sheets. The API does not support setting a column to this type. (This can only be done through the Smartsheet Web app when configuring a project sheet.)                                                                                                                                                                                                                                                                 |
| Predecessor   | PREDECESSOR        | Defines what must happen first in a project flow. For more information, see the Predecessor object. Only for dependency-enabled project sheets                                                                                                                                                                                                                                                                                                               |
| Text/Number   | TEXT_NUMBER        |                                                                                                                                                                                                                                                                                                                                                                                                                                                              |

#### validation (bool)
True if this column must be exactly the type specified, else false
> This is specifically for:
> - Dropdowns (restrict to list values only)
> - Dates (restrict to dates only)
> - Contact Lists (restrict to list values only)
> - Checkboxes (restrict to checkbox use only)
> - Symbols (restrict to symbol values only)