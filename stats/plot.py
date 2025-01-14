import matplotlib.pyplot as plt
import csv
import sys

if len(sys.argv) < 2:
    print("Please provide a CSV file to plot")
    sys.exit(1)

rtt = []
fps = []
cpu = []
gpu = []
memory = []
battery_watt = []
jitter = []

with open(sys.argv[1], mode="r") as file:
    reader = csv.DictReader(file)  # Read the CSV with headers
    for i, row in enumerate(reader):
        if i < 5:
            # Skip first couple frames of test startup
            continue

        # Append data to respective lists
        rtt.append(float(row["RTT"]))
        fps.append(float(row["FPS"]))
        cpu.append(float(row["CPU"]))
        gpu.append(float(row["GPU"]))
        memory.append(float(row["Memory"]))
        battery_watt.append(float(row["BatteryWatt"]))  # Convert to float
        jitter.append(float(row["Jitter"]))  # Convert to float

# Create two subplots
fig, axes = plt.subplots(2, 1, figsize=(10, 10), sharex=True)

# Plot 1: RTT, FPS, GPU, Memory, and Jitter
axes[0].plot(rtt, label="RTT (ms)", marker="o")
axes[0].plot(fps, label="FPS", marker="s")
axes[0].plot(gpu, label="GPU (%)", marker="v")
axes[0].plot(memory, label="Memory (%)", marker="d")
axes[0].plot(jitter, label="Jitter (ms)", marker="p")
axes[0].set_title("RTT, FPS, GPU, Memory, and Jitter")
axes[0].set_xlabel("Sample Index")
axes[0].set_ylabel("Values")
axes[0].legend()
axes[0].grid(True)
axes[0].set_xlim(0, 60)
# axes[0].set_xticks(range(0, 61, 1))

# Plot 2: CPU and Battery Watt
axes[1].plot(cpu, label="CPU (%)", marker="^")
axes[1].plot(battery_watt, label="Battery usage (W)", marker="H")
axes[1].set_title("CPU and Battery Usage")
axes[1].set_ylabel("Values")
axes[1].legend()
axes[1].grid(True)
axes[1].set_xlim(0, 60)
# axes[1].set_xticks(range(0, 61, 1))


plt.tight_layout()
plt.show()
