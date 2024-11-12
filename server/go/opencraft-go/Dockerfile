FROM ubuntu:latest

RUN mkdir /opencraft
# Copy the binary from the builder stage
COPY opencraft-go /opencraft

# Make sure it's executable
RUN chmod +x /opencraft/opencraft-go

WORKDIR /opencraft

# Game port
EXPOSE 7979/udp
# Deployment port
EXPOSE 7980/udp
# Signaling ports
EXPOSE 7981-7984/udp

# Set entrypoint to the Go binary
ENTRYPOINT [ "/opencraft/opencraft-go" ]
