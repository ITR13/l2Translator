import os
import pandas as pd
import sys

def find_files_without_column(folder_path, language_column):
    # Get a list of all CSV files in the folder
    file_list = [file for file in os.listdir(folder_path) if file.endswith('.csv')]

    files_without_column = []

    # Check each CSV file for the language column
    for file in file_list:
        file_path = os.path.join(folder_path, file)

        # Read the CSV file into a DataFrame
        df = pd.read_csv(file_path)

        # Check if the language column exists
        if language_column not in df.columns:
            files_without_column.append(file)

    return files_without_column

def process_csv_files(folder_path, language_column):
    files_without_column = find_files_without_column(folder_path, language_column)

    if files_without_column:
        print("The following files do not have the specified language column:")
        for file in files_without_column:
            print(file)
        sys.exit(1)

    # Get a list of all CSV files in the folder
    file_list = [file for file in os.listdir(folder_path) if file.endswith('.csv')]

    # Process each CSV file
    for file in file_list:
        file_path = os.path.join(folder_path, file)

        # Read the CSV file into a DataFrame
        df = pd.read_csv(file_path)

        # Keep the "Key" and language columns
        columns_to_keep = ['Key', language_column]
        df = df[columns_to_keep]
        df['Reference'] = df[language_column]

        # Save the modified DataFrame back to the CSV file
        df.to_csv(file_path, index=False)

        print(f"Processed file: {file}")

    print(f"Finished processing {len(file_list)} files")

if __name__ == '__main__':
    # Check if folder path and language column name are provided as arguments
    if len(sys.argv) > 2:
        folder_path = sys.argv[1]
        language_column = sys.argv[2]
    else:
        # Provide usage instructions if arguments are missing
        print("Usage: python clean_localization.py folder_path language_column")
        print("folder_path: Path to the folder containing the CSV files.")
        print("language_column: Name of the column to keep in the CSV files.")
        print("Example: python clean_localization.py ./data Language")
        sys.exit(1)

    # Call the function to check for files without the specified column
    process_csv_files(folder_path, language_column)
