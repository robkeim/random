import pandas as pd
import matplotlib.pyplot as plt


def main():
    try:
        # Read the CSV file
        df = pd.read_csv('input.csv')
        print(f"Successfully loaded {len(df)} rows of data")
        print(f"Columns: {list(df.columns)}")
        
        # Check if required columns exist
        if 'Time (seconds)' not in df.columns or 'Heart Rate' not in df.columns:
            print("Error: Required columns 'Time (seconds)' and 'Heart Rate' not found")
            print(f"Available columns: {list(df.columns)}")
            return
        
        # Create the plot
        plt.figure(figsize=(12, 8))
        
        # Convert seconds to minutes for X-axis
        time_minutes = df['Time (seconds)'] / 60
        plt.plot(time_minutes, df['Heart Rate'], 'b-', linewidth=1, alpha=0.8)
        
        # Customize the plot
        plt.title('Heart Rate vs Time During Rowing Session', fontsize=16, fontweight='bold')
        plt.xlabel('Time (minutes)', fontsize=14)
        plt.ylabel('Heart Rate (BPM)', fontsize=14)
        plt.grid(True, alpha=0.3)
        
        # Add some statistics
        avg_hr = df['Heart Rate'].mean()
        max_hr = df['Heart Rate'].max()
        min_hr = df['Heart Rate'].min()
        
        plt.text(0.02, 0.98, f'Average HR: {avg_hr:.1f} BPM\nMax HR: {max_hr} BPM\nMin HR: {min_hr} BPM', 
                transform=plt.gca().transAxes, verticalalignment='top',
                bbox=dict(boxstyle='round', facecolor='wheat', alpha=0.8))
        
        # Show the plot
        plt.tight_layout()
        plt.show()
        
        # Print some summary statistics
        print(f"\nSummary Statistics:")
        total_time_seconds = df['Time (seconds)'].max()
        total_time_minutes = total_time_seconds / 60
        print(f"Total time: {total_time_seconds:.1f} seconds ({total_time_minutes:.1f} minutes)")
        print(f"Average Heart Rate: {avg_hr:.1f} BPM")
        print(f"Maximum Heart Rate: {max_hr} BPM")
        print(f"Minimum Heart Rate: {min_hr} BPM")
    except FileNotFoundError:
        print("Error: input.csv file not found in the current directory")
    except Exception as e:
        print(f"Error reading or processing the file: {e}")


if __name__ == "__main__":
    main()
