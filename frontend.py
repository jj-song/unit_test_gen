import streamlit as st
import openai
import re

# This function checks if the content is a valid C# method
def is_valid_csharp_method(content):
    pattern = r"(public|private|internal|protected)(\s+static)?\s+\w+\s+\w+\("
    return re.search(pattern, content) is not None

# Modularized function to interact with an API (in this case, ChatGPT)
def send_to_api(content, endpoint):
    if endpoint == "ChatGPT":
        prompt = f"Generate NUnit unit tests for the following C# method to maximize code coverage:\n{content}"
        response = openai.ChatCompletion.create(
            model="gpt-3.5-turbo",
            messages=[{"role": "user", "content": prompt},
                      {"role": "system", "content": prompt}],
            temperature=0.7,
            max_tokens=3000
            # n=1,
            # stop=None
        )
        return response['choices'][0]['message']['content']
        
def critique_generated_tests(unit_tests):
    prompt = f"Please critique the following NUnit tests and provide a score out of 10, along with suggestions for improvement:\n\n{unit_tests}. Response should only contain the contents of the unit test and nothing else. "
    response = openai.ChatCompletion.create(
        model="gpt-3.5-turbo",
        messages=[
            {"role": "user", "content": prompt},
            {"role": "system", "content": prompt}
        ],
        temperature=0.7,
        max_tokens=500
    )
    return response['choices'][0]['message']['content']


def main():
    st.title("Unit Test Generation Tool")

    # Input field for OpenAI API key
    openai_key = st.text_input("Enter OpenAI API Key:", type="password")
    if openai_key:
        openai.api_key = openai_key

    uploaded_file = st.file_uploader("Upload C# method (.cs file)", type=["cs"])
    
    # API endpoint selection dropdown
    api_endpoint = st.selectbox("Select API Endpoint", ["ChatGPT", "OtherAPI"])  # Add other API names as required

    if uploaded_file:
        content = uploaded_file.read().decode()
        if is_valid_csharp_method(content):
            if st.button("Generate Unit Tests"):
                unit_tests = send_to_api(content, api_endpoint)
                st.text_area("Generated NUnit Tests", unit_tests, height=300)
                # Save the result to a downloadable file
                with open("generated_tests.cs", "w") as f:
                    f.write(unit_tests)
                st.download_button("Download Generated Tests", "generated_tests.cs", "text/plain")
                if st.button("Critique Generated Tests"):
                    critique = critique_generated_tests(unit_tests)
                    score_pattern = re.search(r"score out of 10: (\d+)", critique)
                    score = score_pattern.group(1) if score_pattern else "N/A"
                    suggestions = critique.replace(score_pattern.group(0), "") if score_pattern else critique
                    st.text_area(f"Score: {score}/10", suggestions, height=150)
        else:
            st.error("The uploaded file does not contain a valid C# method.")

if __name__ == "__main__":
    main()
