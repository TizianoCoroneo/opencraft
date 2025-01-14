import matplotlib.pyplot as plt
import csv
import sys

if sys.argv[1] is None:
    print("Please provide a CSV file to plot")
    sys.exit(1)

rtt = []
fps = []
cpu = []
gpu = []
memory = []
battery_status = []
battery_level = []

with open(sys.argv[1], mode="r") as file:
    reader = csv.DictReader(file)  # Read the CSV with headers
    for row in reader:
        # Skip invalid entries (CPU, GPU, and Memory all 0)
        if (
            float(row["CPU"]) == 0
            and float(row["GPU"]) == 0
            and float(row["Memory"]) == 0
        ):
            continue

        # Append data to respective lists
        rtt.append(float(row["RTT"]))
        fps.append(float(row["FPS"]))
        cpu.append(float(row["CPU"]))
        gpu.append(float(row["GPU"]))
        memory.append(float(row["Memory"]))
        battery_status.append(str(row["BatteryStatus"]))
        battery_level.append(int(row["BatteryLevel"]))


plt.figure(figsize=(10, 6))
plt.plot(rtt, label="RTT (ms)", marker="o")
plt.plot(fps, label="FPS", marker="s")
plt.plot(cpu, label="CPU (%)", marker="^")
plt.plot(gpu, label="GPU (%)", marker="v")
plt.plot(memory, label="Memory (%)", marker="d")
plt.plot(battery_level, label="Battery Level (%)", marker="H")

plt.title("Performance Metrics Over Time")
plt.xlabel("Sample Index")
plt.ylabel("Values")
plt.legend()
plt.grid(True)
plt.tight_layout()

plt.show()
