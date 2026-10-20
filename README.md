# To run the console app  execute below command in terminal

dotnet run --project src/Uuid.Console


# To test from console

dotnet run --project src/Uuid.Console -- 6 2 4 3 1 5 9


# To run tests

dotnet test

# Docker
# Build the console image (tests are skipped unless you target the test stage)
docker build -t uuid-console .

# Optional: run tests during the image build
docker build --target test -t uuid-tests .

# Run the console app with the input as an argument
docker run --rm uuid-console 1 3 2 4
docker run --rm uuid-console "9 20000 1710 2461 9288 10195 10431 12485 12292"
