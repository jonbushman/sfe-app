import React from 'react';

class HexForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      value: ''
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({value: event.target.value});
  }

  handleSubmit(event) {
    alert('An essay was submitted: ' + this.state.value);
    event.preventDefault();
  }

  render() {
    return(
      <input
        type="text"
        value={this.state.value}
        onChange={this.handleChange}
      />
    );
  }
}

class MovementForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {value: '3'};

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({value: event.target.value});
    event.preventDefault();
  }

  handleSubmit(event) {
    alert('Your speed is: ' + this.state.value);
    event.preventDefault();
  }

  render() {
    const hexForms = []
    for (let i = 0; i < this.state.value; i++){
      hexForms.push(<HexForm />)
    }
    const speeds = [1,3,6,9];
    const speedOptions = [];
    for (let i = 0; i < speeds.length; i++){
      speedOptions.push(<option value={speeds[i].toString()}>{speeds[i]}</option>)
    }

    return (
      <div>
        <form onSubmit={this.handleSubmit}>
          <label>
            Speed:
            <select value={this.state.value} onChange={this.handleChange}>
              {speedOptions}
            </select>
            {hexForms}
          </label>
        </form>
        <a className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
        Remove Ships
        </a>
      </div>
    );
  }
}

export default MovementForm
