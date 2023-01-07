import logo from './logo.svg';
import './App.css';
import MovementForm from './components.js';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <MovementForm />
        <MovementForm />
        <MovementForm />
        <a>
        add new Ship Group
        </a>
      </header>
    </div>
  );
}

export default App;
